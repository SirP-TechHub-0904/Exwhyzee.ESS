namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class data6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Topics", "Description", c => c.String());
            AddColumn("dbo.Topics", "DescriptionTitle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Topics", "DescriptionTitle");
            DropColumn("dbo.Topics", "Description");
        }
    }
}
