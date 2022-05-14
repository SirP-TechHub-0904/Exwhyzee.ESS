namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        SurName = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        OtherName = c.String(),
                        PhoneNumber = c.String(nullable: false),
                        ClassLevelId = c.Int(nullable: false),
                        DateRegistered = c.DateTime(),
                        UserId = c.String(maxLength: 128),
                        IskoolId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassLevels", t => t.ClassLevelId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ClassLevelId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentModels", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.StudentModels", "ClassLevelId", "dbo.ClassLevels");
            DropIndex("dbo.StudentModels", new[] { "UserId" });
            DropIndex("dbo.StudentModels", new[] { "ClassLevelId" });
            DropTable("dbo.StudentModels");
        }
    }
}
