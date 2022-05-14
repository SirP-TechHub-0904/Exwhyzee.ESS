namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeacherQualification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeacherEvaluations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject1 = c.String(),
                        Subject2 = c.String(),
                        Subject3 = c.String(),
                        Qualification = c.String(),
                        QualificationFront = c.String(),
                        QualificationBack = c.String(),
                        YouTubeVideo = c.String(),
                        UserProfileId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfileModels", t => t.UserProfileId)
                .Index(t => t.UserProfileId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherEvaluations", "UserProfileId", "dbo.UserProfileModels");
            DropIndex("dbo.TeacherEvaluations", new[] { "UserProfileId" });
            DropTable("dbo.TeacherEvaluations");
        }
    }
}
