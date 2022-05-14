namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedtoDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Topics", "VideoId", c => c.String());
            AddColumn("dbo.Topics", "VideoFile", c => c.String());
            AddColumn("dbo.Topics", "UserId", c => c.String());
            AddColumn("dbo.Topics", "Approved", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserProfileModels", "CV_Views", c => c.Int(nullable: false));
            AddColumn("dbo.UserProfileModels", "Portfolio_Views", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfileModels", "Portfolio_Views");
            DropColumn("dbo.UserProfileModels", "CV_Views");
            DropColumn("dbo.Topics", "Approved");
            DropColumn("dbo.Topics", "UserId");
            DropColumn("dbo.Topics", "VideoFile");
            DropColumn("dbo.Topics", "VideoId");
        }
    }
}
