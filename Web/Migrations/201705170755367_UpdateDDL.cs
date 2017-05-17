namespace Web.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDDL : DbMigration
    {
        public override void Up()
        {
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
                "dbo.CaTypeRegistry",
                c => new
                    {
                        CaTypeRegistryId = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CaTypeRegistryId);
            
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
                "dbo.OptionTypeRegistry",
                c => new
                    {
                        OptionTypeRegistryId = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.OptionTypeRegistryId);
            
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
                "dbo.PayoutTypeRegistry",
                c => new
                    {
                        PayoutTypeRegistryId = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PayoutTypeRegistryId);
            
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
                "dbo.ScrubbingInfo",
                c => new
                    {
                        ScrubbingInfoId = c.Int(nullable: false, identity: true),
                        FieldRegistryId = c.Int(nullable: false),
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
                .PrimaryKey(t => t.ScrubbingInfoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PayoutTypeFieldMap", "PayoutTypeRegistryId", "dbo.PayoutTypeRegistry");
            DropForeignKey("dbo.PayoutTypeFieldMap", "FieldRegistryId", "dbo.FieldRegistry");
            DropForeignKey("dbo.OptionTypeFieldMap", "OptionTypeRegistryId", "dbo.OptionTypeRegistry");
            DropForeignKey("dbo.OptionTypeFieldMap", "FieldRegistryId", "dbo.FieldRegistry");
            DropForeignKey("dbo.CaTypeFieldMap", "FieldRegistryId", "dbo.FieldRegistry");
            DropForeignKey("dbo.CaTypeFieldMap", "CaTypeRegistryId", "dbo.CaTypeRegistry");
            DropIndex("dbo.PayoutTypeFieldMap", new[] { "FieldRegistryId" });
            DropIndex("dbo.PayoutTypeFieldMap", new[] { "PayoutTypeRegistryId" });
            DropIndex("dbo.OptionTypeFieldMap", new[] { "FieldRegistryId" });
            DropIndex("dbo.OptionTypeFieldMap", new[] { "OptionTypeRegistryId" });
            DropIndex("dbo.CaTypeFieldMap", new[] { "FieldRegistryId" });
            DropIndex("dbo.CaTypeFieldMap", new[] { "CaTypeRegistryId" });
            DropTable("dbo.ScrubbingInfo");
            DropTable("dbo.ProcessTypeRegistry");
            DropTable("dbo.PayoutTypeRegistry");
            DropTable("dbo.PayoutTypeFieldMap");
            DropTable("dbo.OptionTypeRegistry");
            DropTable("dbo.OptionTypeFieldMap");
            DropTable("dbo.EventLog");
            DropTable("dbo.DatesConfiguration");
            DropTable("dbo.FieldRegistry");
            DropTable("dbo.CaTypeRegistry");
            DropTable("dbo.CaTypeFieldMap");
            DropTable("dbo.CaTimelineView");
        }
    }
}
