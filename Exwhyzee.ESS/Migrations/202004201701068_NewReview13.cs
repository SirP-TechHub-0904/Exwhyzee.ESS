namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewReview13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfileModels", "PhotoUrl", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfileModels", "PhotoUrl");
        }
    }
}
