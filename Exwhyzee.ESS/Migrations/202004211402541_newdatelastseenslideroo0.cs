namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newdatelastseenslideroo0 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImageSliders", "Link", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ImageSliders", "Link");
        }
    }
}
