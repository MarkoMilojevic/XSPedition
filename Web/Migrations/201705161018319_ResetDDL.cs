namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResetDDL : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        AccountNumber = c.String(nullable: false),
                        AccountOwner = c.String(nullable: false),
                        BusinessEntity = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AccountId);
            
            CreateTable(
                "dbo.Balance",
                c => new
                    {
                        BalanceId = c.Int(nullable: false, identity: true),
                        CaId = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                        IsNotificationSent = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BalanceId)
                .ForeignKey("dbo.Account", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.CorporateAction", t => t.CaId, cascadeDelete: true)
                .Index(t => t.CaId)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.CorporateAction",
                c => new
                    {
                        CaId = c.Int(nullable: false, identity: true),
                        CaTypeRegistryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CaId)
                .ForeignKey("dbo.CaTypeRegistry", t => t.CaTypeRegistryId, cascadeDelete: true)
                .Index(t => t.CaTypeRegistryId);
            
            CreateTable(
                "dbo.CaTypeRegistry",
                c => new
                    {
                        CaTypeRegistryId = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CaTypeRegistryId);
            
            CreateTable(
                "dbo.Option",
                c => new
                    {
                        OptionId = c.Int(nullable: false, identity: true),
                        CaId = c.Int(nullable: false),
                        OptionTypeRegistryId = c.Int(nullable: false),
                        OptionNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OptionId)
                .ForeignKey("dbo.CorporateAction", t => t.CaId, cascadeDelete: true)
                .ForeignKey("dbo.OptionTypeRegistry", t => t.OptionTypeRegistryId, cascadeDelete: true)
                .Index(t => t.CaId)
                .Index(t => t.OptionTypeRegistryId);
            
            CreateTable(
                "dbo.OptionTypeRegistry",
                c => new
                    {
                        OptionTypeRegistryId = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.OptionTypeRegistryId);
            
            CreateTable(
                "dbo.Payout",
                c => new
                    {
                        PayoutId = c.Int(nullable: false, identity: true),
                        OptionId = c.Int(nullable: false),
                        PayoutTypeRegistryId = c.Int(nullable: false),
                        PayoutNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PayoutId)
                .ForeignKey("dbo.Option", t => t.OptionId, cascadeDelete: true)
                .ForeignKey("dbo.PayoutTypeRegistry", t => t.PayoutTypeRegistryId, cascadeDelete: true)
                .Index(t => t.OptionId)
                .Index(t => t.PayoutTypeRegistryId);
            
            CreateTable(
                "dbo.PayoutTypeRegistry",
                c => new
                    {
                        PayoutTypeRegistryId = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PayoutTypeRegistryId);
            
            CreateTable(
                "dbo.CaTimelineView",
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
                "dbo.CaTypeFieldMap",
                c => new
                    {
                        CaTypeRegistryId = c.Int(nullable: false),
                        FieldRegistryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CaTypeRegistryId, t.FieldRegistryId })
                .ForeignKey("dbo.CaTypeRegistry", t => t.CaTypeRegistryId, cascadeDelete: true)
                .ForeignKey("dbo.FieldRegistry", t => t.FieldRegistryId, cascadeDelete: true)
                .Index(t => t.CaTypeRegistryId)
                .Index(t => t.FieldRegistryId);
            
            CreateTable(
                "dbo.FieldRegistry",
                c => new
                    {
                        FieldRegistryId = c.Int(nullable: false, identity: true),
                        FieldDisplay = c.String(nullable: false),
                        FieldType = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.FieldRegistryId);
            
            CreateTable(
                "dbo.DatesConfiguration",
                c => new
                    {
                        DateConfigurationId = c.Int(nullable: false, identity: true),
                        CaType = c.Int(nullable: false),
                        ProcessTypeRegistryId = c.Int(nullable: false),
                        FieldRegistryId = c.Int(nullable: false),
                        DateOffset = c.Int(nullable: false),
                        IsCritical = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DateConfigurationId);
            
            CreateTable(
                "dbo.EventLog",
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
                "dbo.FieldValue",
                c => new
                    {
                        FieldValueId = c.Int(nullable: false, identity: true),
                        FieldRegistryId = c.Int(nullable: false),
                        Value = c.String(),
                        IsScrubbed = c.Boolean(nullable: false),
                        CaId = c.Int(),
                        OptionId = c.Int(),
                        PayoutId = c.Int(),
                    })
                .PrimaryKey(t => t.FieldValueId)
                .ForeignKey("dbo.FieldRegistry", t => t.FieldRegistryId, cascadeDelete: true)
                .Index(t => t.FieldRegistryId);
            
            CreateTable(
                "dbo.InstructionProcessView",
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
                "dbo.NotificationProcessView",
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
                "dbo.OptionTypeFieldMap",
                c => new
                    {
                        OptionTypeRegistryId = c.Int(nullable: false),
                        FieldRegistryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OptionTypeRegistryId, t.FieldRegistryId })
                .ForeignKey("dbo.FieldRegistry", t => t.FieldRegistryId, cascadeDelete: true)
                .ForeignKey("dbo.OptionTypeRegistry", t => t.OptionTypeRegistryId, cascadeDelete: true)
                .Index(t => t.OptionTypeRegistryId)
                .Index(t => t.FieldRegistryId);
            
            CreateTable(
                "dbo.PaymentProcessView",
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
                "dbo.Payment",
                c => new
                    {
                        PaymentId = c.Int(nullable: false, identity: true),
                        ResponseId = c.Int(nullable: false),
                        PayoutId = c.Int(nullable: false),
                        IsSettled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentId)
                .ForeignKey("dbo.Payout", t => t.PayoutId, cascadeDelete: true)
                .ForeignKey("dbo.Response", t => t.ResponseId, cascadeDelete: true)
                .Index(t => t.ResponseId)
                .Index(t => t.PayoutId);
            
            CreateTable(
                "dbo.Response",
                c => new
                    {
                        ResponseId = c.Int(nullable: false),
                        IsInstructed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ResponseId)
                .ForeignKey("dbo.Balance", t => t.ResponseId)
                .Index(t => t.ResponseId);
            
            CreateTable(
                "dbo.PayoutTypeFieldMap",
                c => new
                    {
                        PayoutTypeRegistryId = c.Int(nullable: false),
                        FieldRegistryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PayoutTypeRegistryId, t.FieldRegistryId })
                .ForeignKey("dbo.FieldRegistry", t => t.FieldRegistryId, cascadeDelete: true)
                .ForeignKey("dbo.PayoutTypeRegistry", t => t.PayoutTypeRegistryId, cascadeDelete: true)
                .Index(t => t.PayoutTypeRegistryId)
                .Index(t => t.FieldRegistryId);
            
            CreateTable(
                "dbo.ProcessTypeRegistry",
                c => new
                    {
                        ProcessTypeRegistryId = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Display = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ProcessTypeRegistryId);
            
            CreateTable(
                "dbo.ResponseProcessView",
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
                "dbo.ScrubbingProcessView",
                c => new
                    {
                        ScrubbingProcessViewId = c.Int(nullable: false, identity: true),
                        CaId = c.Int(),
                        OptionId = c.Int(),
                        PayoutId = c.Int(),
                        FieldRegistryId = c.Int(),
                        FieldDisplay = c.String(),
                        ProcessedDateCategory = c.Int(nullable: false),
                        IsSrubbed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ScrubbingProcessViewId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PayoutTypeFieldMap", "PayoutTypeRegistryId", "dbo.PayoutTypeRegistry");
            DropForeignKey("dbo.PayoutTypeFieldMap", "FieldRegistryId", "dbo.FieldRegistry");
            DropForeignKey("dbo.Payment", "ResponseId", "dbo.Response");
            DropForeignKey("dbo.Response", "ResponseId", "dbo.Balance");
            DropForeignKey("dbo.Payment", "PayoutId", "dbo.Payout");
            DropForeignKey("dbo.OptionTypeFieldMap", "OptionTypeRegistryId", "dbo.OptionTypeRegistry");
            DropForeignKey("dbo.OptionTypeFieldMap", "FieldRegistryId", "dbo.FieldRegistry");
            DropForeignKey("dbo.FieldValue", "FieldRegistryId", "dbo.FieldRegistry");
            DropForeignKey("dbo.CaTypeFieldMap", "FieldRegistryId", "dbo.FieldRegistry");
            DropForeignKey("dbo.CaTypeFieldMap", "CaTypeRegistryId", "dbo.CaTypeRegistry");
            DropForeignKey("dbo.Payout", "PayoutTypeRegistryId", "dbo.PayoutTypeRegistry");
            DropForeignKey("dbo.Payout", "OptionId", "dbo.Option");
            DropForeignKey("dbo.Option", "OptionTypeRegistryId", "dbo.OptionTypeRegistry");
            DropForeignKey("dbo.Option", "CaId", "dbo.CorporateAction");
            DropForeignKey("dbo.CorporateAction", "CaTypeRegistryId", "dbo.CaTypeRegistry");
            DropForeignKey("dbo.Balance", "CaId", "dbo.CorporateAction");
            DropForeignKey("dbo.Balance", "AccountId", "dbo.Account");
            DropIndex("dbo.PayoutTypeFieldMap", new[] { "FieldRegistryId" });
            DropIndex("dbo.PayoutTypeFieldMap", new[] { "PayoutTypeRegistryId" });
            DropIndex("dbo.Response", new[] { "ResponseId" });
            DropIndex("dbo.Payment", new[] { "PayoutId" });
            DropIndex("dbo.Payment", new[] { "ResponseId" });
            DropIndex("dbo.OptionTypeFieldMap", new[] { "FieldRegistryId" });
            DropIndex("dbo.OptionTypeFieldMap", new[] { "OptionTypeRegistryId" });
            DropIndex("dbo.FieldValue", new[] { "FieldRegistryId" });
            DropIndex("dbo.CaTypeFieldMap", new[] { "FieldRegistryId" });
            DropIndex("dbo.CaTypeFieldMap", new[] { "CaTypeRegistryId" });
            DropIndex("dbo.Payout", new[] { "PayoutTypeRegistryId" });
            DropIndex("dbo.Payout", new[] { "OptionId" });
            DropIndex("dbo.Option", new[] { "OptionTypeRegistryId" });
            DropIndex("dbo.Option", new[] { "CaId" });
            DropIndex("dbo.CorporateAction", new[] { "CaTypeRegistryId" });
            DropIndex("dbo.Balance", new[] { "AccountId" });
            DropIndex("dbo.Balance", new[] { "CaId" });
            DropTable("dbo.ScrubbingProcessView");
            DropTable("dbo.ResponseProcessView");
            DropTable("dbo.ProcessTypeRegistry");
            DropTable("dbo.PayoutTypeFieldMap");
            DropTable("dbo.Response");
            DropTable("dbo.Payment");
            DropTable("dbo.PaymentProcessView");
            DropTable("dbo.OptionTypeFieldMap");
            DropTable("dbo.NotificationProcessView");
            DropTable("dbo.InstructionProcessView");
            DropTable("dbo.FieldValue");
            DropTable("dbo.EventLog");
            DropTable("dbo.DatesConfiguration");
            DropTable("dbo.FieldRegistry");
            DropTable("dbo.CaTypeFieldMap");
            DropTable("dbo.CaTimelineView");
            DropTable("dbo.PayoutTypeRegistry");
            DropTable("dbo.Payout");
            DropTable("dbo.OptionTypeRegistry");
            DropTable("dbo.Option");
            DropTable("dbo.CaTypeRegistry");
            DropTable("dbo.CorporateAction");
            DropTable("dbo.Balance");
            DropTable("dbo.Account");
        }
    }
}
