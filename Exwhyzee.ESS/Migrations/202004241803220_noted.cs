namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class noted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfileModels", "Verified", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfileModels", "Verified");
        }
    }
}
