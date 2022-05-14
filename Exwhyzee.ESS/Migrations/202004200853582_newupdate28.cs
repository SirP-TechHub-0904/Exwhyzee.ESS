namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newupdate28 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CompleteProfileDtoes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        SurName = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        ContactAddress = c.String(),
                        Country = c.String(),
                        Description = c.String(),
                        DateOfBirth = c.DateTime(),
                        DateRegistered = c.DateTime(nullable: false),
                        UserId = c.String(),
                        FacebookLink = c.String(),
                        TwitterLink = c.String(),
                        LinkedinLink = c.String(),
                        PhotoUrl = c.String(),
                        ComplementryCardKeywords = c.String(),
                        IskoolId = c.String(),
                        Gender = c.String(),
                        MaritalStatus = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.EducationHistoryModels", "CompleteProfileDto_Id", c => c.Long());
            AddColumn("dbo.ExperienceModels", "CompleteProfileDto_Id", c => c.Long());
            AddColumn("dbo.LanguageSpokenModels", "CompleteProfileDto_Id", c => c.Long());
            AddColumn("dbo.MembershipOfLearneredSocietyModels", "CompleteProfileDto_Id", c => c.Long());
            AddColumn("dbo.MeritCertificateModels", "CompleteProfileDto_Id", c => c.Long());
            AddColumn("dbo.SkillModels", "CompleteProfileDto_Id", c => c.Long());
            AddColumn("dbo.TrainingAndWorkShopModels", "CompleteProfileDto_Id", c => c.Long());
            CreateIndex("dbo.EducationHistoryModels", "CompleteProfileDto_Id");
            CreateIndex("dbo.ExperienceModels", "CompleteProfileDto_Id");
            CreateIndex("dbo.LanguageSpokenModels", "CompleteProfileDto_Id");
            CreateIndex("dbo.MembershipOfLearneredSocietyModels", "CompleteProfileDto_Id");
            CreateIndex("dbo.MeritCertificateModels", "CompleteProfileDto_Id");
            CreateIndex("dbo.SkillModels", "CompleteProfileDto_Id");
            CreateIndex("dbo.TrainingAndWorkShopModels", "CompleteProfileDto_Id");
            AddForeignKey("dbo.EducationHistoryModels", "CompleteProfileDto_Id", "dbo.CompleteProfileDtoes", "Id");
            AddForeignKey("dbo.ExperienceModels", "CompleteProfileDto_Id", "dbo.CompleteProfileDtoes", "Id");
            AddForeignKey("dbo.LanguageSpokenModels", "CompleteProfileDto_Id", "dbo.CompleteProfileDtoes", "Id");
            AddForeignKey("dbo.MembershipOfLearneredSocietyModels", "CompleteProfileDto_Id", "dbo.CompleteProfileDtoes", "Id");
            AddForeignKey("dbo.MeritCertificateModels", "CompleteProfileDto_Id", "dbo.CompleteProfileDtoes", "Id");
            AddForeignKey("dbo.SkillModels", "CompleteProfileDto_Id", "dbo.CompleteProfileDtoes", "Id");
            AddForeignKey("dbo.TrainingAndWorkShopModels", "CompleteProfileDto_Id", "dbo.CompleteProfileDtoes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrainingAndWorkShopModels", "CompleteProfileDto_Id", "dbo.CompleteProfileDtoes");
            DropForeignKey("dbo.SkillModels", "CompleteProfileDto_Id", "dbo.CompleteProfileDtoes");
            DropForeignKey("dbo.MeritCertificateModels", "CompleteProfileDto_Id", "dbo.CompleteProfileDtoes");
            DropForeignKey("dbo.MembershipOfLearneredSocietyModels", "CompleteProfileDto_Id", "dbo.CompleteProfileDtoes");
            DropForeignKey("dbo.LanguageSpokenModels", "CompleteProfileDto_Id", "dbo.CompleteProfileDtoes");
            DropForeignKey("dbo.ExperienceModels", "CompleteProfileDto_Id", "dbo.CompleteProfileDtoes");
            DropForeignKey("dbo.EducationHistoryModels", "CompleteProfileDto_Id", "dbo.CompleteProfileDtoes");
            DropIndex("dbo.TrainingAndWorkShopModels", new[] { "CompleteProfileDto_Id" });
            DropIndex("dbo.SkillModels", new[] { "CompleteProfileDto_Id" });
            DropIndex("dbo.MeritCertificateModels", new[] { "CompleteProfileDto_Id" });
            DropIndex("dbo.MembershipOfLearneredSocietyModels", new[] { "CompleteProfileDto_Id" });
            DropIndex("dbo.LanguageSpokenModels", new[] { "CompleteProfileDto_Id" });
            DropIndex("dbo.ExperienceModels", new[] { "CompleteProfileDto_Id" });
            DropIndex("dbo.EducationHistoryModels", new[] { "CompleteProfileDto_Id" });
            DropColumn("dbo.TrainingAndWorkShopModels", "CompleteProfileDto_Id");
            DropColumn("dbo.SkillModels", "CompleteProfileDto_Id");
            DropColumn("dbo.MeritCertificateModels", "CompleteProfileDto_Id");
            DropColumn("dbo.MembershipOfLearneredSocietyModels", "CompleteProfileDto_Id");
            DropColumn("dbo.LanguageSpokenModels", "CompleteProfileDto_Id");
            DropColumn("dbo.ExperienceModels", "CompleteProfileDto_Id");
            DropColumn("dbo.EducationHistoryModels", "CompleteProfileDto_Id");
            DropTable("dbo.CompleteProfileDtoes");
        }
    }
}
