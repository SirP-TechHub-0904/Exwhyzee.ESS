namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class batch : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SchoolPortalDatas", "BatchResultPrint", c => c.String());
            AddColumn("dbo.SessionAnalysis", "PortalUrl", c => c.String());
            AddColumn("dbo.TermlyAnalysis", "PortalUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TermlyAnalysis", "PortalUrl");
            DropColumn("dbo.SessionAnalysis", "PortalUrl");
            DropColumn("dbo.SchoolPortalDatas", "BatchResultPrint");
        }
    }
}
