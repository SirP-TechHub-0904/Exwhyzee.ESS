namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edu2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "FullName", c => c.String());
            AddColumn("dbo.Questions", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "Date");
            DropColumn("dbo.Questions", "FullName");
        }
    }
}
