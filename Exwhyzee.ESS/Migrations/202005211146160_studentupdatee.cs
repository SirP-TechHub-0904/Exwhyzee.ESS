namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentupdatee : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StudentModels", "ClassLevelId", "dbo.ClassLevels");
            DropIndex("dbo.StudentModels", new[] { "ClassLevelId" });
            AlterColumn("dbo.StudentModels", "Email", c => c.String());
            AlterColumn("dbo.StudentModels", "ClassLevelId", c => c.Int());
            CreateIndex("dbo.StudentModels", "ClassLevelId");
            AddForeignKey("dbo.StudentModels", "ClassLevelId", "dbo.ClassLevels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentModels", "ClassLevelId", "dbo.ClassLevels");
            DropIndex("dbo.StudentModels", new[] { "ClassLevelId" });
            AlterColumn("dbo.StudentModels", "ClassLevelId", c => c.Int(nullable: false));
            AlterColumn("dbo.StudentModels", "Email", c => c.String(nullable: false));
            CreateIndex("dbo.StudentModels", "ClassLevelId");
            AddForeignKey("dbo.StudentModels", "ClassLevelId", "dbo.ClassLevels", "Id", cascadeDelete: true);
        }
    }
}
