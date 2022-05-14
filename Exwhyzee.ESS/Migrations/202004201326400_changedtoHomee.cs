namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedtoHomee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserReviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Review = c.String(),
                        UserId = c.String(),
                        ReviewerId = c.String(),
                        DateReviewed = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserReviews");
        }
    }
}
