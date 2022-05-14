namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentPicture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentModels", "Picture", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentModels", "Picture");
        }
    }
}
