namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedtoHome : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfileModels", "AdminReview", c => c.String());
            AddColumn("dbo.UserProfileModels", "AdminApproval", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfileModels", "AdminApproval");
            DropColumn("dbo.UserProfileModels", "AdminReview");
        }
    }
}
