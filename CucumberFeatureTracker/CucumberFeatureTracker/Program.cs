using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace CucumberFeatureTracker
{

    public struct FileAndPath
    {
        public string File;
        public string Path;
    }

    class Program
    {

        enum FeatureTags
        {
            @DailyTest,
            @WeeklyTest
        }

        static void Main(string[] args)
        {
            var context = new FeatureContext();
            List<FileAndPath> featureFiles = new List<FileAndPath>();
            List<FeatureTag> featureTagList = new List<FeatureTag>();

            string featurePath = System.Configuration.ConfigurationManager.AppSettings["dspApps_features"].ToString();

            try
            {
                // Console.WriteLine should be outside the GetFilesInFolderAndSubfolders because it is a recursive function
                Console.WriteLine("Reading feature file names");
                GetFilesInFolderAndSubfolders(featurePath, featureFiles);

                UpsertFeatureFilesTable(context, featureFiles);

                ReadFeatureTagsFromFiles(context);

                UpsertFeatureTagsTable(context);

                GetCucumberYmlSections(context);

                UpsertCucumberSections(context);

                GetCucumberFeatures(context);

                UpsertCucumberFeatures(context);

                UpdateIsInCucumberYmlFlag(context);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Press RETURN to finish");
            Console.ReadKey();
        }

        private static void GetFilesInFolderAndSubfolders(string path, List<FileAndPath> featureFiles)
        {
            FileAndPath ff;

            foreach (string filename in Directory.GetFiles(path))
            {
                ff.Path = path;
                ff.File = filename;
                featureFiles.Add(ff);
            }
            foreach (string d in Directory.GetDirectories(path))
            {
                GetFilesInFolderAndSubfolders(d, featureFiles);
            }
        }

        private static void UpsertFeatureFilesTable(FeatureContext context, List<FileAndPath> featureFiles)
        {
            FeatureFile featureFile;

            Console.WriteLine("Deleting FeatureFiles table");
            context.Database.ExecuteSqlCommand("DELETE FROM FeatureFiles");

            Console.WriteLine("Populating FeatureFiles table");
            foreach (FileAndPath ff in featureFiles)
            {
                featureFile = new FeatureFile
                {
                    FeatureFileName = Path.GetFileName(ff.File),
                    Path = ff.Path
                };
                context.FeatureFiles.Add(featureFile);
                //Console.WriteLine(ff.File);
            }
            context.SaveChanges();

        }

        private static void ReadFeatureTagsFromFiles(FeatureContext context)
        {
            StreamReader sr;
            FeatureTag ft;
            string fullPath;
            string firstLine;
            string _tag;

            char[] delimiterChars = { '@' };

            Console.WriteLine("Reading feature files to retrieve tags");
            foreach (var ff in context.FeatureFiles.ToList())
            {
                fullPath = Path.Combine(ff.Path, ff.FeatureFileName);
                sr = new StreamReader(fullPath);
                firstLine = sr.ReadLine();

                if (firstLine.Contains('@'))
                {
                    string[] tags = firstLine.Split(delimiterChars);
                    foreach (string tag in tags)
                    {
                        if (!string.IsNullOrEmpty(tag))
                        {
                            _tag = "@" + tag.Replace(" ", "");

                            ft = new FeatureTag
                            {
                                FeatureFileName = ff.FeatureFileName,
                                Tag = _tag,
                                FeatureID = ff.ID,
                                MyFeatureFile = ff
                            };
                            context.FeatureTags.Add(ft);
                        }
                    }
                }
                sr.Close();
            }
        }

        private static void UpsertFeatureTagsTable(FeatureContext context)
        {
            Console.WriteLine("Deleting FeatureTags table");
            context.Database.ExecuteSqlCommand("DELETE FROM FeatureTags");

            Console.WriteLine("Populating FeatureTags table");
            context.SaveChanges();
        }

        private static void GetCucumberYmlSections(FeatureContext context)
        {
            CucumberSection cs;
            List<CucumberSection> csList = new List<CucumberSection>();

            string cucumber_yml = System.Configuration.ConfigurationManager.AppSettings["cucumber.yml"].ToString();

            string[] lines = File.ReadAllLines(cucumber_yml);
            char cFirst;
            char cLast;
            int order = 1;

            Console.WriteLine("Reading cucumber.yml sections");
            foreach (string line in lines)
            {
                if (line.Length > 0)
                {
                    cLast = line[line.Length - 1];
                    cFirst = line[0];
                }
                else
                {
                    continue;
                }

                // Lines that finish with colon and start different than # or space will be considered sections
                if (cLast == ':' && cFirst != ' ' && cFirst != '#')
                {
                    cs = new CucumberSection
                    {
                        SectionName = FormatCucumberSection(line),
                        Order = order
                    };
                    csList.Add(cs);
                    order++;
                }
            }

            context.CucumberSection.AddRange(csList);
        }

        private static void UpsertCucumberSections(FeatureContext context)
        {
            Console.WriteLine("Deleting CucumberSections table");
            context.Database.ExecuteSqlCommand("DELETE FROM CucumberSections");

            Console.WriteLine("Populating CucumberSections table");
            context.SaveChanges();
        }

        private static void GetCucumberFeatures(FeatureContext context)
        {
            FeaturesAtCucumberYml fc;
            List<FeaturesAtCucumberYml> fcList = new List<FeaturesAtCucumberYml>();

            string cucumber_yml = System.Configuration.ConfigurationManager.AppSettings["cucumber.yml"].ToString();

            string[] lines = File.ReadAllLines(cucumber_yml);
            string currentSection = "";
            string featureName;
            char cFirst;
            char cLast;
            int order = 1;

            Console.WriteLine("Reading cucumber.yml features");
            foreach (string line in lines)
            {
                if (line.Length > 0)
                {
                    cLast = line[line.Length - 1];
                    cFirst = line[0];
                }
                else
                {
                    continue;
                }

                // Lines that finish with colon and start different than # or space will be considered sections
                if (cLast == ':' && cFirst != ' ' && cFirst != '#')
                {
                    currentSection = line.Replace(":", String.Empty);
                }
                else if (LineIsFeature(line))
                {
                    featureName = GetFeatureNameFromLine(line);

                    fc = new FeaturesAtCucumberYml
                    {
                        FeatureFileName = featureName,
                        SectionName = currentSection,
                        Order = order
                    };
                    fcList.Add(fc);
                }

            }
            context.FeaturesAtCucumberYml.AddRange(fcList);
        }

        private static void UpsertCucumberFeatures(FeatureContext context)
        {
            Console.WriteLine("Deleting FeaturesAtCucumberYml table");
            context.Database.ExecuteSqlCommand("DELETE FROM FeaturesAtCucumberYml");

            Console.WriteLine("Populating FeatureAtCucumberYml table");
            context.SaveChanges();
        }

        private static void UpdateIsInCucumberYmlFlag(FeatureContext context)
        {
            const string SQL_Command = "UPDATE FeatureFiles SET IsInCucumberYml = 1 WHERE FeatureFileName IN (SELECT FeatureFileName FROM FeaturesAtCucumberYml)";

            Console.WriteLine("Updating the IsInCucumberYml flag at FeatureFiles table");
            context.Database.ExecuteSqlCommand(SQL_Command);
        }

        // SUPPORT METHODS:

        private static string FormatCucumberSection(string UnformatedCucumberSection)
        {
            return UnformatedCucumberSection.Replace(":", String.Empty);
        }

        private static bool LineIsFeature(string line)
        {
            line = line.Replace(" ", String.Empty);
            if (line.Length < 8)
                return false;

            if (line.Substring(line.Length-8) == ".feature")
                return true;
            else
                return false;

        }

        private static string GetFeatureNameFromLine(string line)
        {
            char[] delimiterChars = { '\\' };
            string[] parts = line.Split(delimiterChars);
            string featureName;

            line = line.Replace(" ", String.Empty);
            if (parts.Length <= 1)
                throw new Exception(String.Format("Line \n {0} \ndoes not contain a feature", line));

            featureName = parts[parts.Length - 1];

            //Console.WriteLine(line.Substring(line.Length - 8));
            if (line.Substring(line.Length - 8) != ".feature")
                throw new Exception(String.Format("Line \n{0} \ndoes not contain a feature", line));

            return featureName;
        }

    }
}
