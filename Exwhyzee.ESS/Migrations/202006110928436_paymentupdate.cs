namespace Exwhyzee.ESS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class paymentupdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SMSSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SmsUsername = c.String(),
                        SmsPassword = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubscriptionCommisions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        TeacherCommission = c.Decimal(precision: 18, scale: 2),
                        SystemCommission = c.Decimal(precision: 18, scale: 2),
                        SubjectId = c.Int(),
                        TeacherId = c.Long(),
                        StudentModelId = c.Int(),
                        ClassLevelId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LiveClassLevels", t => t.ClassLevelId)
                .ForeignKey("dbo.StudentModels", t => t.StudentModelId)
                .ForeignKey("dbo.LiveClassSubjects", t => t.SubjectId)
                .ForeignKey("dbo.UserProfileModels", t => t.TeacherId)
                .Index(t => t.SubjectId)
                .Index(t => t.TeacherId)
                .Index(t => t.StudentModelId)
                .Index(t => t.ClassLevelId);
            
            CreateTable(
                "dbo.TeacherLiveSubjectAssignments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubjectId = c.Int(),
                        TeacherId = c.Long(),
                        ClassId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LiveClassLevels", t => t.ClassId)
                .ForeignKey("dbo.LiveClassSubjects", t => t.SubjectId)
                .ForeignKey("dbo.UserProfileModels", t => t.TeacherId)
                .Index(t => t.SubjectId)
                .Index(t => t.TeacherId)
                .Index(t => t.ClassId);
            
            CreateTable(
                "dbo.TeacherWallets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        UserId = c.String(maxLength: 128),
                        TeacherId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfileModels", t => t.TeacherId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.TeacherWithdrawals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        AccountName = c.String(),
                        AccountNumber = c.String(),
                        BankName = c.String(),
                        RequestDate = c.DateTime(nullable: false),
                        ApprovedDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        TeacherId = c.Long(),
                        ApprovedbyId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfileModels", t => t.ApprovedbyId)
                .ForeignKey("dbo.UserProfileModels", t => t.TeacherId)
                .Index(t => t.TeacherId)
                .Index(t => t.ApprovedbyId);
            
            AddColumn("dbo.Subscriptions", "StudentModelId", c => c.Int());
            CreateIndex("dbo.Subscriptions", "StudentModelId");
            AddForeignKey("dbo.Subscriptions", "StudentModelId", "dbo.StudentModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherWithdrawals", "TeacherId", "dbo.UserProfileModels");
            DropForeignKey("dbo.TeacherWithdrawals", "ApprovedbyId", "dbo.UserProfileModels");
            DropForeignKey("dbo.TeacherWallets", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeacherWallets", "TeacherId", "dbo.UserProfileModels");
            DropForeignKey("dbo.TeacherLiveSubjectAssignments", "TeacherId", "dbo.UserProfileModels");
            DropForeignKey("dbo.TeacherLiveSubjectAssignments", "SubjectId", "dbo.LiveClassSubjects");
            DropForeignKey("dbo.TeacherLiveSubjectAssignments", "ClassId", "dbo.LiveClassLevels");
            DropForeignKey("dbo.Subscriptions", "StudentModelId", "dbo.StudentModels");
            DropForeignKey("dbo.SubscriptionCommisions", "TeacherId", "dbo.UserProfileModels");
            DropForeignKey("dbo.SubscriptionCommisions", "SubjectId", "dbo.LiveClassSubjects");
            DropForeignKey("dbo.SubscriptionCommisions", "StudentModelId", "dbo.StudentModels");
            DropForeignKey("dbo.SubscriptionCommisions", "ClassLevelId", "dbo.LiveClassLevels");
            DropIndex("dbo.TeacherWithdrawals", new[] { "ApprovedbyId" });
            DropIndex("dbo.TeacherWithdrawals", new[] { "TeacherId" });
            DropIndex("dbo.TeacherWallets", new[] { "TeacherId" });
            DropIndex("dbo.TeacherWallets", new[] { "UserId" });
            DropIndex("dbo.TeacherLiveSubjectAssignments", new[] { "ClassId" });
            DropIndex("dbo.TeacherLiveSubjectAssignments", new[] { "TeacherId" });
            DropIndex("dbo.TeacherLiveSubjectAssignments", new[] { "SubjectId" });
            DropIndex("dbo.Subscriptions", new[] { "StudentModelId" });
            DropIndex("dbo.SubscriptionCommisions", new[] { "ClassLevelId" });
            DropIndex("dbo.SubscriptionCommisions", new[] { "StudentModelId" });
            DropIndex("dbo.SubscriptionCommisions", new[] { "TeacherId" });
            DropIndex("dbo.SubscriptionCommisions", new[] { "SubjectId" });
            DropColumn("dbo.Subscriptions", "StudentModelId");
            DropTable("dbo.TeacherWithdrawals");
            DropTable("dbo.TeacherWallets");
            DropTable("dbo.TeacherLiveSubjectAssignments");
            DropTable("dbo.SubscriptionCommisions");
            DropTable("dbo.SMSSettings");
        }
    }
}
