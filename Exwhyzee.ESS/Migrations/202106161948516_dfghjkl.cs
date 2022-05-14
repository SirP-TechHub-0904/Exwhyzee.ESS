namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dfghjkl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SchoolSessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Session = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.SchoolPortalDatas", "AddAsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SchoolPortalDatas", "AddAsActive");
            DropTable("dbo.SchoolSessions");
        }
    }
}
