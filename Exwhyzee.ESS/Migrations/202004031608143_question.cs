namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class question : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuestionCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.QuestionMains",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sort = c.Int(nullable: false),
                        Img = c.String(),
                        TextQ = c.String(),
                        OptionA = c.String(),
                        OptionB = c.String(),
                        OptionC = c.String(),
                        OptionD = c.String(),
                        Answer = c.String(),
                        UserType_Id = c.Int(),
                        QuestionCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.UserType_Id)
                .ForeignKey("dbo.QuestionCategories", t => t.QuestionCategory_Id)
                .Index(t => t.UserType_Id)
                .Index(t => t.QuestionCategory_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionMains", "QuestionCategory_Id", "dbo.QuestionCategories");
            DropForeignKey("dbo.QuestionMains", "UserType_Id", "dbo.Questions");
            DropIndex("dbo.QuestionMains", new[] { "QuestionCategory_Id" });
            DropIndex("dbo.QuestionMains", new[] { "UserType_Id" });
            DropTable("dbo.QuestionMains");
            DropTable("dbo.QuestionCategories");
        }
    }
}
