namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class simulationsubjectupdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SimulationCategories", "SimulationCategories_Id", "dbo.SimulationCategories");
            DropIndex("dbo.SimulationCategories", new[] { "SimulationCategories_Id" });
            DropColumn("dbo.SimulationCategories", "SimulationCategories_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SimulationCategories", "SimulationCategories_Id", c => c.Int());
            CreateIndex("dbo.SimulationCategories", "SimulationCategories_Id");
            AddForeignKey("dbo.SimulationCategories", "SimulationCategories_Id", "dbo.SimulationCategories", "Id");
        }
    }
}
