namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SimulationSubjects : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SimulationSubjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.SimulationCategories", "SimulationSubjectId", c => c.Int());
            AddColumn("dbo.SimulationCategories", "SimulationCategories_Id", c => c.Int());
            CreateIndex("dbo.SimulationCategories", "SimulationSubjectId");
            CreateIndex("dbo.SimulationCategories", "SimulationCategories_Id");
            AddForeignKey("dbo.SimulationCategories", "SimulationCategories_Id", "dbo.SimulationCategories", "Id");
            AddForeignKey("dbo.SimulationCategories", "SimulationSubjectId", "dbo.SimulationSubjects", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SimulationCategories", "SimulationSubjectId", "dbo.SimulationSubjects");
            DropForeignKey("dbo.SimulationCategories", "SimulationCategories_Id", "dbo.SimulationCategories");
            DropIndex("dbo.SimulationCategories", new[] { "SimulationCategories_Id" });
            DropIndex("dbo.SimulationCategories", new[] { "SimulationSubjectId" });
            DropColumn("dbo.SimulationCategories", "SimulationCategories_Id");
            DropColumn("dbo.SimulationCategories", "SimulationSubjectId");
            DropTable("dbo.SimulationSubjects");
        }
    }
}
