namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewReview135 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SubjectSpecialistModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        UserProfileId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SubjectSpecialistModels");
        }
    }
}
