namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edu : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassLevels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ClassLevelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassLevels", t => t.ClassLevelId, cascadeDelete: true)
                .Index(t => t.ClassLevelId);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        VideoUrl = c.String(),
                        VideoCover = c.String(),
                        Publish = c.Boolean(nullable: false),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        SchoolName = c.String(),
                        State = c.String(),
                        QuestionContent = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Topics", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Subjects", "ClassLevelId", "dbo.ClassLevels");
            DropIndex("dbo.Topics", new[] { "SubjectId" });
            DropIndex("dbo.Subjects", new[] { "ClassLevelId" });
            DropTable("dbo.Questions");
            DropTable("dbo.Topics");
            DropTable("dbo.Subjects");
            DropTable("dbo.ClassLevels");
        }
    }
}
