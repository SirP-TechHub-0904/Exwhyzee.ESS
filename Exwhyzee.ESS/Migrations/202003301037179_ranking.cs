namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ranking : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SchoolRankings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Position = c.Int(nullable: false),
                        Name = c.String(),
                        SchoolType = c.String(),
                        State = c.String(),
                        IT = c.String(),
                        Website = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Volunteers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SectionType = c.Int(nullable: false),
                        CoverPhoto = c.String(),
                        VideoLink = c.String(),
                        Approved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "School", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "School");
            DropTable("dbo.Volunteers");
            DropTable("dbo.SchoolRankings");
        }
    }
}
