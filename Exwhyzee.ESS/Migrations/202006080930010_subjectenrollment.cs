namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subjectenrollment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LiveClassSubjectEnrollments", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LiveClassSubjectEnrollments", "Status");
        }
    }
}
