namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _65748374h7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SchoolCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.SchoolPortalDatas", "SelectedAsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.SchoolPortalDatas", "SchoolCategoryId", c => c.Int());
            CreateIndex("dbo.SchoolPortalDatas", "SchoolCategoryId");
            AddForeignKey("dbo.SchoolPortalDatas", "SchoolCategoryId", "dbo.SchoolCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SchoolPortalDatas", "SchoolCategoryId", "dbo.SchoolCategories");
            DropIndex("dbo.SchoolPortalDatas", new[] { "SchoolCategoryId" });
            DropColumn("dbo.SchoolPortalDatas", "SchoolCategoryId");
            DropColumn("dbo.SchoolPortalDatas", "SelectedAsActive");
            DropTable("dbo.SchoolCategories");
        }
    }
}
