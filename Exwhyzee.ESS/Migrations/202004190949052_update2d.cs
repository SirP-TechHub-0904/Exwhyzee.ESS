namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update2d : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DataImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageLink = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DataImages");
        }
    }
}
