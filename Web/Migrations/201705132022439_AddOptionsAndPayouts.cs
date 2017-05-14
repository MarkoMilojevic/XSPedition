namespace Web.Migrations
{
	using System.Data.Entity.Migrations;
    
    public partial class AddOptionsAndPayouts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Options",
                c => new
                    {
                        OptionId = c.Int(nullable: false, identity: true),
                        CaId = c.Int(nullable: false),
                        OptionTypeLookupId = c.Int(nullable: false),
                        OptionNumber = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.OptionId)
                .ForeignKey("dbo.CorporateActions", t => t.CaId, cascadeDelete: true)
                .ForeignKey("dbo.OptionTypeLookups", t => t.OptionTypeLookupId, cascadeDelete: true)
                .Index(t => t.CaId)
                .Index(t => t.OptionTypeLookupId);
            
            CreateTable(
                "dbo.Payouts",
                c => new
                    {
                        PayoutId = c.Int(nullable: false, identity: true),
                        OptionId = c.Int(nullable: false),
                        PayoutTypeLookupId = c.Int(nullable: false),
                        PayoutNumber = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PayoutId)
                .ForeignKey("dbo.Options", t => t.OptionId, cascadeDelete: true)
                .ForeignKey("dbo.PayoutTypeLookups", t => t.PayoutTypeLookupId, cascadeDelete: true)
                .Index(t => t.OptionId)
                .Index(t => t.PayoutTypeLookupId);
            
            AlterColumn("dbo.FieldValues", "Value", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payouts", "PayoutTypeLookupId", "dbo.PayoutTypeLookups");
            DropForeignKey("dbo.Payouts", "OptionId", "dbo.Options");
            DropForeignKey("dbo.Options", "OptionTypeLookupId", "dbo.OptionTypeLookups");
            DropForeignKey("dbo.Options", "CaId", "dbo.CorporateActions");
            DropIndex("dbo.Payouts", new[] { "PayoutTypeLookupId" });
            DropIndex("dbo.Payouts", new[] { "OptionId" });
            DropIndex("dbo.Options", new[] { "OptionTypeLookupId" });
            DropIndex("dbo.Options", new[] { "CaId" });
            AlterColumn("dbo.FieldValues", "Value", c => c.String());
            DropTable("dbo.Payouts");
            DropTable("dbo.Options");
        }
    }
}
