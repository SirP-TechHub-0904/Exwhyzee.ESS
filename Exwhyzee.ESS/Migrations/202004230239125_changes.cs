namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subjects", "Description", c => c.String());
            AddColumn("dbo.Subjects", "CoverImage", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subjects", "CoverImage");
            DropColumn("dbo.Subjects", "Description");
        }
    }
}
