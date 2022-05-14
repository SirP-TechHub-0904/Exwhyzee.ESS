namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DegreeObtainedModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        Abbr = c.String(),
                        Date = c.String(),
                        UserProfileId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EducationHistoryModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SchoolAttended = c.String(),
                        Course = c.String(),
                        StartDate = c.String(),
                        EndDate = c.String(),
                        Grade = c.String(),
                        IsCurrent = c.Boolean(nullable: false),
                        UserProfileId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExperienceModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        ExperienceType = c.String(),
                        Address = c.String(),
                        StartDate = c.String(),
                        EndDate = c.String(),
                        Description = c.String(),
                        IsCurrent = c.Boolean(nullable: false),
                        UserProfileId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InterpersonalSkillModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        UserProfileId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.JobAnalysisModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        Count = c.String(),
                        UserProfileId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LanguageSpokenModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        UserProfileId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MembershipOfLearneredSocietyModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        Abbr = c.String(),
                        UserProfileId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MeritCertificateModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        StartDate = c.String(),
                        UserProfileId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RefereeModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Office = c.String(),
                        Name = c.String(),
                        EmailAndPhone = c.String(),
                        UserProfileId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RenderedServiceModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        UserProfileId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SkillModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        UserProfileId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Testimonies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Message = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TrainingAndWorkShopModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        Abbr = c.String(),
                        Location = c.String(),
                        Date = c.String(),
                        UserProfileId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WorkHistoryModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        VideoUrl = c.String(),
                        Location = c.String(),
                        UserProfileId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WorkHistoryModels");
            DropTable("dbo.TrainingAndWorkShopModels");
            DropTable("dbo.Testimonies");
            DropTable("dbo.SkillModels");
            DropTable("dbo.RenderedServiceModels");
            DropTable("dbo.RefereeModels");
            DropTable("dbo.MeritCertificateModels");
            DropTable("dbo.MembershipOfLearneredSocietyModels");
            DropTable("dbo.LanguageSpokenModels");
            DropTable("dbo.JobAnalysisModels");
            DropTable("dbo.InterpersonalSkillModels");
            DropTable("dbo.ExperienceModels");
            DropTable("dbo.EducationHistoryModels");
            DropTable("dbo.DegreeObtainedModels");
        }
    }
}
