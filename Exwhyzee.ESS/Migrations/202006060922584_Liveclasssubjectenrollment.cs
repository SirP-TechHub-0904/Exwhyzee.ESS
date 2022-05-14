namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Liveclasssubjectenrollment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LiveClassSubjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        LiveClassLevelId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LiveClassLevels", t => t.LiveClassLevelId)
                .Index(t => t.LiveClassLevelId);
            
            CreateTable(
                "dbo.LiveClassSubjectEnrollments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        LiveClassSubjectId = c.Int(),
                        LiveClassLevelId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LiveClassLevels", t => t.LiveClassLevelId)
                .ForeignKey("dbo.LiveClassSubjects", t => t.LiveClassSubjectId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.LiveClassSubjectId)
                .Index(t => t.LiveClassLevelId);
            
            AddColumn("dbo.Subscriptions", "LiveClassSubjectId", c => c.Int());
            CreateIndex("dbo.Subscriptions", "LiveClassSubjectId");
            AddForeignKey("dbo.Subscriptions", "LiveClassSubjectId", "dbo.LiveClassSubjects", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subscriptions", "LiveClassSubjectId", "dbo.LiveClassSubjects");
            DropForeignKey("dbo.LiveClassSubjectEnrollments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.LiveClassSubjectEnrollments", "LiveClassSubjectId", "dbo.LiveClassSubjects");
            DropForeignKey("dbo.LiveClassSubjectEnrollments", "LiveClassLevelId", "dbo.LiveClassLevels");
            DropForeignKey("dbo.LiveClassSubjects", "LiveClassLevelId", "dbo.LiveClassLevels");
            DropIndex("dbo.Subscriptions", new[] { "LiveClassSubjectId" });
            DropIndex("dbo.LiveClassSubjectEnrollments", new[] { "LiveClassLevelId" });
            DropIndex("dbo.LiveClassSubjectEnrollments", new[] { "LiveClassSubjectId" });
            DropIndex("dbo.LiveClassSubjectEnrollments", new[] { "UserId" });
            DropIndex("dbo.LiveClassSubjects", new[] { "LiveClassLevelId" });
            DropColumn("dbo.Subscriptions", "LiveClassSubjectId");
            DropTable("dbo.LiveClassSubjectEnrollments");
            DropTable("dbo.LiveClassSubjects");
        }
    }
}
