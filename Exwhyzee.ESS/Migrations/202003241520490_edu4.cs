namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edu4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClassLevels", "SchoolType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClassLevels", "SchoolType");
        }
    }
}
