namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TopicReview : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TopicReviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Review = c.String(),
                        Rating = c.Int(),
                        Name = c.String(),
                        Email = c.String(),
                        TopicId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Topics", t => t.TopicId)
                .Index(t => t.TopicId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TopicReviews", "TopicId", "dbo.Topics");
            DropIndex("dbo.TopicReviews", new[] { "TopicId" });
            DropTable("dbo.TopicReviews");
        }
    }
}
