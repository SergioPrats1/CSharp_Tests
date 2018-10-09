using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CucumberFeatureTracker
{
    public class FeatureContext : DbContext
    {
        const int FeatureFileNameLength = 500;
        const int TagNameLength = 200;
        const int SectionNameLength = 200;


        public FeatureContext() : base("Test")
        { }

        public DbSet<FeatureFile> FeatureFiles { get; set; }
        public DbSet<FeatureTag> FeatureTags { get; set; }
        public DbSet<CucumberSection> CucumberSection { get; set; }
        public DbSet<FeaturesAtCucumberYml> FeaturesAtCucumberYml { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FeatureFile>()
                .Property(c => c.FeatureFileName)
                .IsRequired()
                .HasMaxLength(FeatureFileNameLength);
            
            modelBuilder.Entity<FeatureTag>()
                .Property(c => c.FeatureFileName)
                .IsRequired()
                .HasMaxLength(FeatureFileNameLength);

            modelBuilder.Entity<FeatureTag>()
                .Property(c => c.Tag)
                .IsRequired()
                .HasMaxLength(TagNameLength);

            modelBuilder.Entity<FeatureFile>()
                .HasMany(f => f.FeatureTags)
                .WithRequired(t => t.MyFeatureFile)
                .HasForeignKey(t => t.FeatureID);

            modelBuilder.Entity<CucumberSection>()
                .Property(c => c.SectionName)
                .IsRequired()
                .HasMaxLength(FeatureFileNameLength);

            modelBuilder.Entity<CucumberSection>()
                .HasKey(c => c.SectionName);

            modelBuilder.Entity<FeaturesAtCucumberYml>()
                .Property(c => c.SectionName)
                .IsRequired()
                .HasMaxLength(SectionNameLength);

            modelBuilder.Entity<FeaturesAtCucumberYml>()
                .Property(c => c.FeatureFileName)
                .IsRequired()
                .HasMaxLength(FeatureFileNameLength);

            modelBuilder.Entity<FeaturesAtCucumberYml>()
                .HasKey( f => new { f.SectionName, f.FeatureFileName } );

            modelBuilder.Entity<FeaturesAtCucumberYml>().ToTable("FeaturesAtCucumberYml");

            base.OnModelCreating(modelBuilder);
        }

    }
}
