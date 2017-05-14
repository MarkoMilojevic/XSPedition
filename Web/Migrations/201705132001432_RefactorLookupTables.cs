namespace Web.Migrations
{
	using System.Data.Entity.Migrations;
    
    public partial class RefactorLookupTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CorporateActions", "CaTypeId", "dbo.CaTypes");
            DropIndex("dbo.CorporateActions", new[] { "CaTypeId" });
            CreateTable(
                "dbo.CaTypeLookups",
                c => new
                    {
                        CaTypeLookupId = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CaTypeLookupId);
            
            CreateTable(
                "dbo.OptionTypeLookups",
                c => new
                    {
                        OptionTypeLookupId = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.OptionTypeLookupId);
            
            CreateTable(
                "dbo.PayoutTypeLookups",
                c => new
                    {
                        PayoutTypeLookupId = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PayoutTypeLookupId);
            
            AddColumn("dbo.CorporateActions", "CaTypeLookupId", c => c.Int(nullable: false));
            CreateIndex("dbo.CorporateActions", "CaTypeLookupId");
            AddForeignKey("dbo.CorporateActions", "CaTypeLookupId", "dbo.CaTypeLookups", "CaTypeLookupId", cascadeDelete: true);
            DropColumn("dbo.CorporateActions", "CaTypeId");
            DropTable("dbo.CaTypes");
            DropTable("dbo.OptionTypes");
            DropTable("dbo.PayoutTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PayoutTypes",
                c => new
                    {
                        PayoutTypeId = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PayoutTypeId);
            
            CreateTable(
                "dbo.OptionTypes",
                c => new
                    {
                        OptionTypeId = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.OptionTypeId);
            
            CreateTable(
                "dbo.CaTypes",
                c => new
                    {
                        CaTypeId = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CaTypeId);
            
            AddColumn("dbo.CorporateActions", "CaTypeId", c => c.Int(nullable: false));
            DropForeignKey("dbo.CorporateActions", "CaTypeLookupId", "dbo.CaTypeLookups");
            DropIndex("dbo.CorporateActions", new[] { "CaTypeLookupId" });
            DropColumn("dbo.CorporateActions", "CaTypeLookupId");
            DropTable("dbo.PayoutTypeLookups");
            DropTable("dbo.OptionTypeLookups");
            DropTable("dbo.CaTypeLookups");
            CreateIndex("dbo.CorporateActions", "CaTypeId");
            AddForeignKey("dbo.CorporateActions", "CaTypeId", "dbo.CaTypes", "CaTypeId", cascadeDelete: true);
        }
    }
}
