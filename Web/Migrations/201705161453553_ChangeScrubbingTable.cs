namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeScrubbingTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Balance", "AccountId", "dbo.Account");
            DropForeignKey("dbo.Balance", "CaId", "dbo.CorporateAction");
            DropForeignKey("dbo.CorporateAction", "CaTypeRegistryId", "dbo.CaTypeRegistry");
            DropForeignKey("dbo.Option", "CaId", "dbo.CorporateAction");
            DropForeignKey("dbo.Option", "OptionTypeRegistryId", "dbo.OptionTypeRegistry");
            DropForeignKey("dbo.Payout", "OptionId", "dbo.Option");
            DropForeignKey("dbo.Payout", "PayoutTypeRegistryId", "dbo.PayoutTypeRegistry");
            DropForeignKey("dbo.FieldValue", "FieldRegistryId", "dbo.FieldRegistry");
            DropForeignKey("dbo.Payment", "PayoutId", "dbo.Payout");
            DropForeignKey("dbo.Response", "ResponseId", "dbo.Balance");
            DropForeignKey("dbo.Payment", "ResponseId", "dbo.Response");
            DropIndex("dbo.Balance", new[] { "CaId" });
            DropIndex("dbo.Balance", new[] { "AccountId" });
            DropIndex("dbo.CorporateAction", new[] { "CaTypeRegistryId" });
            DropIndex("dbo.Option", new[] { "CaId" });
            DropIndex("dbo.Option", new[] { "OptionTypeRegistryId" });
            DropIndex("dbo.Payout", new[] { "OptionId" });
            DropIndex("dbo.Payout", new[] { "PayoutTypeRegistryId" });
            DropIndex("dbo.FieldValue", new[] { "FieldRegistryId" });
            DropIndex("dbo.Payment", new[] { "ResponseId" });
            DropIndex("dbo.Payment", new[] { "PayoutId" });
            DropIndex("dbo.Response", new[] { "ResponseId" });
            CreateTable(
                "dbo.ScrubbingInfo",
                c => new
                    {
                        ScrubbingId = c.Int(nullable: false, identity: true),
                        CaId = c.Int(nullable: false),
                        CaTypeId = c.Int(),
                        OptionNumber = c.Int(),
                        OptionTypeId = c.Int(),
                        PayoutNumber = c.Int(),
                        PayoutTypeId = c.Int(),
                        FieldDisplay = c.String(),
                        ProcessedDateCategory = c.Int(nullable: false),
                        IsSrubbed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ScrubbingId);
            
            DropTable("dbo.Account");
            DropTable("dbo.Balance");
            DropTable("dbo.CorporateAction");
            DropTable("dbo.Option");
            DropTable("dbo.Payout");
            DropTable("dbo.FieldValue");
            DropTable("dbo.InstructionProcessView");
            DropTable("dbo.NotificationProcessView");
            DropTable("dbo.PaymentProcessView");
            DropTable("dbo.Payment");
            DropTable("dbo.Response");
            DropTable("dbo.ResponseProcessView");
            DropTable("dbo.ScrubbingProcessView");
        }
        
        public override void Down()
        {
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
                "dbo.Response",
                c => new
                    {
                        ResponseId = c.Int(nullable: false),
                        IsInstructed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ResponseId);
            
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        PaymentId = c.Int(nullable: false, identity: true),
                        ResponseId = c.Int(nullable: false),
                        PayoutId = c.Int(nullable: false),
                        IsSettled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentId);
            
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
                .PrimaryKey(t => t.FieldValueId);
            
            CreateTable(
                "dbo.Payout",
                c => new
                    {
                        PayoutId = c.Int(nullable: false, identity: true),
                        OptionId = c.Int(nullable: false),
                        PayoutTypeRegistryId = c.Int(nullable: false),
                        PayoutNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PayoutId);
            
            CreateTable(
                "dbo.Option",
                c => new
                    {
                        OptionId = c.Int(nullable: false, identity: true),
                        CaId = c.Int(nullable: false),
                        OptionTypeRegistryId = c.Int(nullable: false),
                        OptionNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OptionId);
            
            CreateTable(
                "dbo.CorporateAction",
                c => new
                    {
                        CaId = c.Int(nullable: false, identity: true),
                        CaTypeRegistryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CaId);
            
            CreateTable(
                "dbo.Balance",
                c => new
                    {
                        BalanceId = c.Int(nullable: false, identity: true),
                        CaId = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                        IsNotificationSent = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BalanceId);
            
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
            
            DropTable("dbo.ScrubbingInfo");
            CreateIndex("dbo.Response", "ResponseId");
            CreateIndex("dbo.Payment", "PayoutId");
            CreateIndex("dbo.Payment", "ResponseId");
            CreateIndex("dbo.FieldValue", "FieldRegistryId");
            CreateIndex("dbo.Payout", "PayoutTypeRegistryId");
            CreateIndex("dbo.Payout", "OptionId");
            CreateIndex("dbo.Option", "OptionTypeRegistryId");
            CreateIndex("dbo.Option", "CaId");
            CreateIndex("dbo.CorporateAction", "CaTypeRegistryId");
            CreateIndex("dbo.Balance", "AccountId");
            CreateIndex("dbo.Balance", "CaId");
            AddForeignKey("dbo.Payment", "ResponseId", "dbo.Response", "ResponseId", cascadeDelete: true);
            AddForeignKey("dbo.Response", "ResponseId", "dbo.Balance", "BalanceId");
            AddForeignKey("dbo.Payment", "PayoutId", "dbo.Payout", "PayoutId", cascadeDelete: true);
            AddForeignKey("dbo.FieldValue", "FieldRegistryId", "dbo.FieldRegistry", "FieldRegistryId", cascadeDelete: true);
            AddForeignKey("dbo.Payout", "PayoutTypeRegistryId", "dbo.PayoutTypeRegistry", "PayoutTypeRegistryId", cascadeDelete: true);
            AddForeignKey("dbo.Payout", "OptionId", "dbo.Option", "OptionId", cascadeDelete: true);
            AddForeignKey("dbo.Option", "OptionTypeRegistryId", "dbo.OptionTypeRegistry", "OptionTypeRegistryId", cascadeDelete: true);
            AddForeignKey("dbo.Option", "CaId", "dbo.CorporateAction", "CaId", cascadeDelete: true);
            AddForeignKey("dbo.CorporateAction", "CaTypeRegistryId", "dbo.CaTypeRegistry", "CaTypeRegistryId", cascadeDelete: true);
            AddForeignKey("dbo.Balance", "CaId", "dbo.CorporateAction", "CaId", cascadeDelete: true);
            AddForeignKey("dbo.Balance", "AccountId", "dbo.Account", "AccountId", cascadeDelete: true);
        }
    }
}
