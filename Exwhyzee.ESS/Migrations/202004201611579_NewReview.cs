namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewReview : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserReviews", "Rating", c => c.Int(nullable: false));
            AlterColumn("dbo.UserProfileModels", "AdminApproval", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserProfileModels", "AdminApproval", c => c.String());
            DropColumn("dbo.UserReviews", "Rating");
        }
    }
}
