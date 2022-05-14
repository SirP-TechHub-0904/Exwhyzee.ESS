namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class withdrawalupdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TeacherWithdrawals", "RequestDate", c => c.DateTime());
            AlterColumn("dbo.TeacherWithdrawals", "ApprovedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TeacherWithdrawals", "ApprovedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TeacherWithdrawals", "RequestDate", c => c.DateTime(nullable: false));
        }
    }
}
