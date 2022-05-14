namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewReview1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserProfileModels", "PhotoUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfileModels", "PhotoUrl", c => c.String());
        }
    }
}
