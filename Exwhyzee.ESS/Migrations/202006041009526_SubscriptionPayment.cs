namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubscriptionPayment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subscriptions", "Amount", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Subscriptions", "ReferenceId", c => c.String());
            AddColumn("dbo.Subscriptions", "ClassLevelId", c => c.Int());
            AddColumn("dbo.ZoomSettings", "SubscriptionAmount", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Subscriptions", "StartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Subscriptions", "EndDate", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Subscriptions", "ClassLevelId");
            AddForeignKey("dbo.Subscriptions", "ClassLevelId", "dbo.LiveClassLevels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subscriptions", "ClassLevelId", "dbo.LiveClassLevels");
            DropIndex("dbo.Subscriptions", new[] { "ClassLevelId" });
            AlterColumn("dbo.Subscriptions", "EndDate", c => c.DateTime());
            AlterColumn("dbo.Subscriptions", "StartDate", c => c.DateTime());
            DropColumn("dbo.ZoomSettings", "SubscriptionAmount");
            DropColumn("dbo.Subscriptions", "ClassLevelId");
            DropColumn("dbo.Subscriptions", "ReferenceId");
            DropColumn("dbo.Subscriptions", "Amount");
        }
    }
}
