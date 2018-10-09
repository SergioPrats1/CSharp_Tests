using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CucumberFeatureTracker
{
    public class FeatureFile
    {
        public int ID { get; set; }
        public string FeatureFileName { get; set; }
        public string Path { get; set; }
        public bool IsInCucumberYml { get; set; }

        // Relationships
        public ICollection<FeatureTag> FeatureTags { get; set; }
    }

    public class FeatureTag
    {
        public int ID { get; set; }
        public string FeatureFileName { get; set; }
        public string Tag { get; set; }
        public int FeatureID { get; set; }

        // Relationships
        public FeatureFile MyFeatureFile { get; set; }
    }

    public class CucumberSection
    {
        public string SectionName { get; set; }
        public int Order { get; set; }
    }

    public class FeaturesAtCucumberYml
    {
        //public int ID { get; set; }
        public string FeatureFileName { get; set; }
        public string SectionName { get; set; }
        public int Order { get; set; }
    }
}
