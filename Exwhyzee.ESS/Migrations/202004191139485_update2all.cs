namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update2all : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProfileModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        SurName = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        ContactAddress = c.String(),
                        Country = c.String(),
                        Description = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        DateRegistered = c.DateTime(nullable: false),
                        UserId = c.String(),
                        FacebookLink = c.String(),
                        TwitterLink = c.String(),
                        LinkedinLink = c.String(),
                        PhotoUrl = c.String(),
                        ComplementryCardKeywords = c.String(),
                        LojourId = c.String(),
                        Gender = c.String(),
                        MaritalStatus = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserProfileModels");
        }
    }
}
