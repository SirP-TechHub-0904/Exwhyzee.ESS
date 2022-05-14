namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class schools : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NPSTschools",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SchoolName = c.String(),
                        ContactAddress = c.String(),
                        EmailAddress = c.String(),
                        Phone = c.String(),
                        WebsiteLink = c.String(),
                        HeadName = c.String(),
                        PopulationSize = c.Int(nullable: false),
                        Zone = c.String(),
                        State = c.String(),
                        LGA = c.String(),
                        Type = c.Int(nullable: false),
                        About = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NPSTschools");
        }
    }
}
