namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newupdate2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserProfileModels", "DateOfBirth", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserProfileModels", "DateOfBirth", c => c.DateTime(nullable: false));
        }
    }
}
