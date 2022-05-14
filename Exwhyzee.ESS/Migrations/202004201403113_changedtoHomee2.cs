namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedtoHomee2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserProfileModels", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.UserProfileModels", "UserId");
            AddForeignKey("dbo.UserProfileModels", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProfileModels", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserProfileModels", new[] { "UserId" });
            AlterColumn("dbo.UserProfileModels", "UserId", c => c.String());
        }
    }
}
