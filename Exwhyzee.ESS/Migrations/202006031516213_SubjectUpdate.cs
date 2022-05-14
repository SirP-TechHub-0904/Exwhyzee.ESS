namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubjectUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OnlineZooms", "LiveClassLevelId", "dbo.LiveClassLevels");
            DropIndex("dbo.OnlineZooms", new[] { "LiveClassLevelId" });
            AlterColumn("dbo.OnlineZooms", "LiveClassLevelId", c => c.Int());
            CreateIndex("dbo.OnlineZooms", "LiveClassLevelId");
            AddForeignKey("dbo.OnlineZooms", "LiveClassLevelId", "dbo.LiveClassLevels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OnlineZooms", "LiveClassLevelId", "dbo.LiveClassLevels");
            DropIndex("dbo.OnlineZooms", new[] { "LiveClassLevelId" });
            AlterColumn("dbo.OnlineZooms", "LiveClassLevelId", c => c.Int(nullable: false));
            CreateIndex("dbo.OnlineZooms", "LiveClassLevelId");
            AddForeignKey("dbo.OnlineZooms", "LiveClassLevelId", "dbo.LiveClassLevels", "Id", cascadeDelete: true);
        }
    }
}
