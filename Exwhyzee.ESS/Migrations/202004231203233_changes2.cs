namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LocalGovs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LGAName = c.String(),
                        StatesId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.States", t => t.StatesId)
                .Index(t => t.StatesId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StateName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UserProfileModels", "State", c => c.String());
            AddColumn("dbo.UserProfileModels", "LGA", c => c.String());
            AddColumn("dbo.UserProfileModels", "Religion", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LocalGovs", "StatesId", "dbo.States");
            DropIndex("dbo.LocalGovs", new[] { "StatesId" });
            DropColumn("dbo.UserProfileModels", "Religion");
            DropColumn("dbo.UserProfileModels", "LGA");
            DropColumn("dbo.UserProfileModels", "State");
            DropTable("dbo.States");
            DropTable("dbo.LocalGovs");
        }
    }
}
