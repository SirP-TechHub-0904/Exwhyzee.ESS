namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update2020 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParticipantResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Won = c.Boolean(nullable: false),
                        Q1 = c.Boolean(nullable: false),
                        Q2 = c.Boolean(nullable: false),
                        Q3 = c.Boolean(nullable: false),
                        Q4 = c.Boolean(nullable: false),
                        Q5 = c.Boolean(nullable: false),
                        ParticipantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Participants", t => t.ParticipantId, cascadeDelete: true)
                .Index(t => t.ParticipantId);
            
            DropColumn("dbo.Participants", "Won");
            DropColumn("dbo.Participants", "Q1");
            DropColumn("dbo.Participants", "Q2");
            DropColumn("dbo.Participants", "Q3");
            DropColumn("dbo.Participants", "Q4");
            DropColumn("dbo.Participants", "Q5");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Participants", "Q5", c => c.Boolean(nullable: false));
            AddColumn("dbo.Participants", "Q4", c => c.Boolean(nullable: false));
            AddColumn("dbo.Participants", "Q3", c => c.Boolean(nullable: false));
            AddColumn("dbo.Participants", "Q2", c => c.Boolean(nullable: false));
            AddColumn("dbo.Participants", "Q1", c => c.Boolean(nullable: false));
            AddColumn("dbo.Participants", "Won", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.ParticipantResults", "ParticipantId", "dbo.Participants");
            DropIndex("dbo.ParticipantResults", new[] { "ParticipantId" });
            DropTable("dbo.ParticipantResults");
        }
    }
}
