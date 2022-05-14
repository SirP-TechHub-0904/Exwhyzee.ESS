namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subscriptionteacherwallet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubscriptionCommisions", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.SubscriptionCommisions", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.SubscriptionCommisions", "SubscriptionId", c => c.Int());
            AddColumn("dbo.Subscriptions", "Email", c => c.String());
            AddColumn("dbo.Subscriptions", "ApprovedbyId", c => c.Long());
            CreateIndex("dbo.SubscriptionCommisions", "SubscriptionId");
            CreateIndex("dbo.Subscriptions", "ApprovedbyId");
            AddForeignKey("dbo.Subscriptions", "ApprovedbyId", "dbo.UserProfileModels", "Id");
            AddForeignKey("dbo.SubscriptionCommisions", "SubscriptionId", "dbo.Subscriptions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubscriptionCommisions", "SubscriptionId", "dbo.Subscriptions");
            DropForeignKey("dbo.Subscriptions", "ApprovedbyId", "dbo.UserProfileModels");
            DropIndex("dbo.Subscriptions", new[] { "ApprovedbyId" });
            DropIndex("dbo.SubscriptionCommisions", new[] { "SubscriptionId" });
            DropColumn("dbo.Subscriptions", "ApprovedbyId");
            DropColumn("dbo.Subscriptions", "Email");
            DropColumn("dbo.SubscriptionCommisions", "SubscriptionId");
            DropColumn("dbo.SubscriptionCommisions", "EndDate");
            DropColumn("dbo.SubscriptionCommisions", "StartDate");
        }
    }
}
