namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes2stated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfileModels", "ShowTeacher", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfileModels", "ShowTeacher");
        }
    }
}
