namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDenormilizedViewsAndEventLog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CaTimelineViews",
                c => new
                    {
                        CaTimelineViewId = c.Int(nullable: false, identity: true),
                        CaId = c.Int(nullable: false),
                        ScrubbingTarget = c.DateTime(nullable: false),
                        ScrubbingCritical = c.DateTime(nullable: false),
                        NotificationTarget = c.DateTime(nullable: false),
                        NotificationCritical = c.DateTime(nullable: false),
                        ResponseTarget = c.DateTime(nullable: false),
                        ResponseCritical = c.DateTime(nullable: false),
                        InstructionTarget = c.DateTime(nullable: false),
                        InstructionCritical = c.DateTime(nullable: false),
                        PaymentTarget = c.DateTime(nullable: false),
                        PaymentCritical = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CaTimelineViewId);
            
            CreateTable(
                "dbo.EventLogs",
                c => new
                    {
                        EventLogId = c.Int(nullable: false, identity: true),
                        CaId = c.Int(nullable: false),
                        ProcessTypeLookupId = c.Int(nullable: false),
                        EventLogInfo = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EventLogId);
            
            CreateTable(
                "dbo.InstructionProcessViews",
                c => new
                    {
                        InstructionProcessViewId = c.Int(nullable: false, identity: true),
                        CaId = c.Int(nullable: false),
                        ResponseId = c.Int(nullable: false),
                        FieldDisplay = c.String(),
                        ProcessedDateCategory = c.Int(nullable: false),
                        IsInstructed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.InstructionProcessViewId);
            
            CreateTable(
                "dbo.NotificationProcessViews",
                c => new
                    {
                        NotificationProcessViewId = c.Int(nullable: false, identity: true),
                        CaId = c.Int(nullable: false),
                        BalanceId = c.Int(nullable: false),
                        FieldDisplay = c.String(),
                        ProcessedDateCategory = c.Int(nullable: false),
                        IsNotificationSent = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.NotificationProcessViewId);
            
            CreateTable(
                "dbo.PaymentProcessViews",
                c => new
                    {
                        PaymentProcessViewId = c.Int(nullable: false, identity: true),
                        CaId = c.Int(nullable: false),
                        ResponseId = c.Int(nullable: false),
                        PayoutId = c.Int(nullable: false),
                        FieldDisplay = c.String(),
                        ProcessedDateCategory = c.Int(nullable: false),
                        IsSettled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentProcessViewId);
            
            CreateTable(
                "dbo.ResponseProcessViews",
                c => new
                    {
                        ResponseProcessViewId = c.Int(nullable: false, identity: true),
                        CaId = c.Int(nullable: false),
                        BalanceId = c.Int(nullable: false),
                        FieldDisplay = c.String(),
                        ProcessedDateCategory = c.Int(nullable: false),
                        IsSubmitted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ResponseProcessViewId);
            
            CreateTable(
                "dbo.ScrubbingProcessViews",
                c => new
                    {
                        ScrubbingProcessViewId = c.Int(nullable: false, identity: true),
                        CaId = c.Int(),
                        OptionId = c.Int(),
                        PayoutId = c.Int(),
                        FieldDisplay = c.String(),
                        ProcessedDateCategory = c.Int(nullable: false),
                        IsSrubbed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ScrubbingProcessViewId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ScrubbingProcessViews");
            DropTable("dbo.ResponseProcessViews");
            DropTable("dbo.PaymentProcessViews");
            DropTable("dbo.NotificationProcessViews");
            DropTable("dbo.InstructionProcessViews");
            DropTable("dbo.EventLogs");
            DropTable("dbo.CaTimelineViews");
        }
    }
}
