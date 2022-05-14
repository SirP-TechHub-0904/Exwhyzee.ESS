namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uptodate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImageSliders", "OrderSort", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ImageSliders", "OrderSort");
        }
    }
}
