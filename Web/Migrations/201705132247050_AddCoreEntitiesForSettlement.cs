namespace Web.Migrations
{
	using System.Data.Entity.Migrations;
    
    public partial class AddCoreEntitiesForSettlement : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        AccountNumber = c.String(nullable: false),
                        AccountOwner = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AccountId);
            
            CreateTable(
                "dbo.Balances",
                c => new
                    {
                        BalanceId = c.Int(nullable: false, identity: true),
                        CaId = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                        IsNotificationSent = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BalanceId)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.CorporateActions", t => t.CaId, cascadeDelete: true)
                .Index(t => t.CaId)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.DatesConfigurations",
                c => new
                    {
                        DateConfigurationId = c.Int(nullable: false, identity: true),
                        CaType = c.Int(nullable: false),
                        ProcessTypeLookupId = c.Int(nullable: false),
                        FieldLookupId = c.Int(nullable: false),
                        DateOffset = c.Int(nullable: false),
                        IsCritical = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DateConfigurationId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        PaymentId = c.Int(nullable: false, identity: true),
                        ResponseId = c.Int(nullable: false),
                        PayoutId = c.Int(nullable: false),
                        IsSettled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentId)
                .ForeignKey("dbo.Payouts", t => t.PayoutId, cascadeDelete: true)
                .ForeignKey("dbo.Responses", t => t.ResponseId, cascadeDelete: true)
                .Index(t => t.ResponseId)
                .Index(t => t.PayoutId);
            
            CreateTable(
                "dbo.Responses",
                c => new
                    {
                        ResponseId = c.Int(nullable: false),
                        IsInstructed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ResponseId)
                .ForeignKey("dbo.Balances", t => t.ResponseId)
                .Index(t => t.ResponseId);
            
            CreateTable(
                "dbo.ProcessTypeLookups",
                c => new
                    {
                        ProcessTypeLookupId = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Display = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ProcessTypeLookupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "ResponseId", "dbo.Responses");
            DropForeignKey("dbo.Responses", "ResponseId", "dbo.Balances");
            DropForeignKey("dbo.Payments", "PayoutId", "dbo.Payouts");
            DropForeignKey("dbo.Balances", "CaId", "dbo.CorporateActions");
            DropForeignKey("dbo.Balances", "AccountId", "dbo.Accounts");
            DropIndex("dbo.Responses", new[] { "ResponseId" });
            DropIndex("dbo.Payments", new[] { "PayoutId" });
            DropIndex("dbo.Payments", new[] { "ResponseId" });
            DropIndex("dbo.Balances", new[] { "AccountId" });
            DropIndex("dbo.Balances", new[] { "CaId" });
            DropTable("dbo.ProcessTypeLookups");
            DropTable("dbo.Responses");
            DropTable("dbo.Payments");
            DropTable("dbo.DatesConfigurations");
            DropTable("dbo.Balances");
            DropTable("dbo.Accounts");
        }
    }
}
