namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class question16 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Participants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        School = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Date = c.DateTime(nullable: false),
                        Won = c.Boolean(nullable: false),
                        Q1 = c.Boolean(nullable: false),
                        Q2 = c.Boolean(nullable: false),
                        Q3 = c.Boolean(nullable: false),
                        Q4 = c.Boolean(nullable: false),
                        Q5 = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Participants");
        }
    }
}
