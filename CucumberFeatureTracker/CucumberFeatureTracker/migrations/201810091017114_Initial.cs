namespace CucumberFeatureTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CucumberSections",
                c => new
                    {
                        SectionName = c.String(nullable: false, maxLength: 500),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SectionName);
            
            CreateTable(
                "dbo.FeatureFiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FeatureFileName = c.String(nullable: false, maxLength: 500),
                        Path = c.String(),
                        IsInCucumberYml = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.FeatureTags",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FeatureFileName = c.String(nullable: false, maxLength: 500),
                        Tag = c.String(nullable: false, maxLength: 200),
                        FeatureID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FeatureFiles", t => t.FeatureID, cascadeDelete: true)
                .Index(t => t.FeatureID);
            
            CreateTable(
                "dbo.FeaturesAtCucumberYml",
                c => new
                    {
                        SectionName = c.String(nullable: false, maxLength: 200),
                        FeatureFileName = c.String(nullable: false, maxLength: 500),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SectionName, t.FeatureFileName });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FeatureTags", "FeatureID", "dbo.FeatureFiles");
            DropIndex("dbo.FeatureTags", new[] { "FeatureID" });
            DropTable("dbo.FeaturesAtCucumberYml");
            DropTable("dbo.FeatureTags");
            DropTable("dbo.FeatureFiles");
            DropTable("dbo.CucumberSections");
        }
    }
}
