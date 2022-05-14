namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Joinrecords : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ZoomStudentJoinedRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        StudentModelId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StudentModels", t => t.StudentModelId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.StudentModelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ZoomStudentJoinedRecords", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ZoomStudentJoinedRecords", "StudentModelId", "dbo.StudentModels");
            DropIndex("dbo.ZoomStudentJoinedRecords", new[] { "StudentModelId" });
            DropIndex("dbo.ZoomStudentJoinedRecords", new[] { "UserId" });
            DropTable("dbo.ZoomStudentJoinedRecords");
        }
    }
}
