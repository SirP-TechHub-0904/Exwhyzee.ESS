namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class liveClassUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LiveClassOnlines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LiveClassLevelId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        UrlLive = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        ClassDate = c.String(),
                        ClassTime = c.String(),
                        Duration = c.String(),
                        TeacherName = c.String(),
                        Description = c.String(),
                        LiveStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LiveClassLevels", t => t.LiveClassLevelId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.LiveClassLevelId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.OnlineZooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HostEmail = c.String(),
                        UserId = c.String(maxLength: 128),
                        MeetingId = c.Long(),
                        MeetingUUId = c.String(),
                        ClassDate = c.String(),
                        ClassTime = c.String(),
                        Duration = c.String(),
                        LiveClassLevelId = c.Int(nullable: false),
                        Description = c.String(),
                        ClassPassword = c.String(),
                        MeetingType = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LiveClassLevels", t => t.LiveClassLevelId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.LiveClassLevelId);
            
            CreateTable(
                "dbo.ZoomHostMailSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ZoomHostEmail = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OnlineZooms", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.OnlineZooms", "LiveClassLevelId", "dbo.LiveClassLevels");
            DropForeignKey("dbo.LiveClassOnlines", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.LiveClassOnlines", "LiveClassLevelId", "dbo.LiveClassLevels");
            DropIndex("dbo.OnlineZooms", new[] { "LiveClassLevelId" });
            DropIndex("dbo.OnlineZooms", new[] { "UserId" });
            DropIndex("dbo.LiveClassOnlines", new[] { "UserId" });
            DropIndex("dbo.LiveClassOnlines", new[] { "LiveClassLevelId" });
            DropTable("dbo.ZoomHostMailSettings");
            DropTable("dbo.OnlineZooms");
            DropTable("dbo.LiveClassOnlines");
        }
    }
}
