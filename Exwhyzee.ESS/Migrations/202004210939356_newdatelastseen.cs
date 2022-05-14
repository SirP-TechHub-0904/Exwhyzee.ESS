namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newdatelastseen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "LastSeen", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "LastSeen");
        }
    }
}
