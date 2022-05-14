namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newdatelastseenslider : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImageSliders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        ContentType = c.String(),
                        Content = c.Binary(),
                        CurrentSlider = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ImageSliders");
        }
    }
}
