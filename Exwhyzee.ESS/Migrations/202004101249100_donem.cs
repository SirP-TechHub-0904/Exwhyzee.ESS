namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class donem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParticipantResults", "Recharged", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ParticipantResults", "Recharged");
        }
    }
}
