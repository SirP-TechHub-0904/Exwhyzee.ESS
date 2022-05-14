namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class question1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuestionMains", "QuestionCategory_Id", "dbo.QuestionCategories");
            DropIndex("dbo.QuestionMains", new[] { "QuestionCategory_Id" });
            RenameColumn(table: "dbo.QuestionMains", name: "QuestionCategory_Id", newName: "QuestionCategoryId");
            AlterColumn("dbo.QuestionMains", "QuestionCategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.QuestionMains", "QuestionCategoryId");
            AddForeignKey("dbo.QuestionMains", "QuestionCategoryId", "dbo.QuestionCategories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionMains", "QuestionCategoryId", "dbo.QuestionCategories");
            DropIndex("dbo.QuestionMains", new[] { "QuestionCategoryId" });
            AlterColumn("dbo.QuestionMains", "QuestionCategoryId", c => c.Int());
            RenameColumn(table: "dbo.QuestionMains", name: "QuestionCategoryId", newName: "QuestionCategory_Id");
            CreateIndex("dbo.QuestionMains", "QuestionCategory_Id");
            AddForeignKey("dbo.QuestionMains", "QuestionCategory_Id", "dbo.QuestionCategories", "Id");
        }
    }
}
