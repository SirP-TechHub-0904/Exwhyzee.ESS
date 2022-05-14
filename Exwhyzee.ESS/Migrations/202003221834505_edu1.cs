namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edu1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Topics", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Topics", "Date");
        }
    }
}
