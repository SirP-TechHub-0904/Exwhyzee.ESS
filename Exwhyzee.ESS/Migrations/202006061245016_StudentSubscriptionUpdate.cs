namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentSubscriptionUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Subscriptions", "LiveClassSubjectId", "dbo.LiveClassSubjects");
            DropIndex("dbo.Subscriptions", new[] { "LiveClassSubjectId" });
            CreateTable(
                "dbo.Properties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UnitsPerSms = c.Decimal(precision: 18, scale: 2),
                        UnitsInAccount = c.Decimal(precision: 18, scale: 2),
                        SmsUsername = c.String(),
                        SmsPassword = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SmsReports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SendTo = c.String(),
                        SenderId = c.String(maxLength: 11),
                        GroupName = c.String(),
                        Message = c.String(),
                        Comment = c.String(),
                        DateSent = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.LiveClassSubjectEnrollments", "StudentModelId", c => c.Int());
            AddColumn("dbo.OnlineZooms", "LiveClassSubjectId", c => c.Int());
            AddColumn("dbo.StudentModels", "ParentPhoneNumber", c => c.String(nullable: false));
            CreateIndex("dbo.LiveClassSubjectEnrollments", "StudentModelId");
            CreateIndex("dbo.OnlineZooms", "LiveClassSubjectId");
            AddForeignKey("dbo.LiveClassSubjectEnrollments", "StudentModelId", "dbo.StudentModels", "Id");
            AddForeignKey("dbo.OnlineZooms", "LiveClassSubjectId", "dbo.LiveClassSubjects", "Id");
            DropColumn("dbo.OnlineZooms", "Subject");
            DropColumn("dbo.Subscriptions", "LiveClassSubjectId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subscriptions", "LiveClassSubjectId", c => c.Int());
            AddColumn("dbo.OnlineZooms", "Subject", c => c.String());
            DropForeignKey("dbo.OnlineZooms", "LiveClassSubjectId", "dbo.LiveClassSubjects");
            DropForeignKey("dbo.LiveClassSubjectEnrollments", "StudentModelId", "dbo.StudentModels");
            DropIndex("dbo.OnlineZooms", new[] { "LiveClassSubjectId" });
            DropIndex("dbo.LiveClassSubjectEnrollments", new[] { "StudentModelId" });
            DropColumn("dbo.StudentModels", "ParentPhoneNumber");
            DropColumn("dbo.OnlineZooms", "LiveClassSubjectId");
            DropColumn("dbo.LiveClassSubjectEnrollments", "StudentModelId");
            DropTable("dbo.SmsReports");
            DropTable("dbo.Properties");
            CreateIndex("dbo.Subscriptions", "LiveClassSubjectId");
            AddForeignKey("dbo.Subscriptions", "LiveClassSubjectId", "dbo.LiveClassSubjects", "Id");
        }
    }
}
