namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfileModels", "IskoolId", c => c.String());
            DropColumn("dbo.UserProfileModels", "LojourId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfileModels", "LojourId", c => c.String());
            DropColumn("dbo.UserProfileModels", "IskoolId");
        }
    }
}
