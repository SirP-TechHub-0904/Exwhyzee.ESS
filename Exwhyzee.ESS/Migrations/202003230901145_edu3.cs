namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edu3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Topics", "SortOrder", c => c.Int(nullable: false));
            AddColumn("dbo.Topics", "Views", c => c.Int(nullable: false));
            AddColumn("dbo.Topics", "Duration", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Topics", "Duration");
            DropColumn("dbo.Topics", "Views");
            DropColumn("dbo.Topics", "SortOrder");
        }
    }
}
