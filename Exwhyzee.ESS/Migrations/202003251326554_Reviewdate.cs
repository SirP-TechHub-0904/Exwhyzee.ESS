namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reviewdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TopicReviews", "DateReview", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TopicReviews", "DateReview");
        }
    }
}
