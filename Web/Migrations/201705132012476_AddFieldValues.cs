namespace Web.Migrations
{
	using System.Data.Entity.Migrations;
    
    public partial class AddFieldValues : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FieldLookups",
                c => new
                    {
                        FieldLookupId = c.Int(nullable: false, identity: true),
                        FieldDisplay = c.String(nullable: false),
                        FieldType = c.String(nullable: false),
                        IsRequired = c.Boolean(nullable: false),
                        CaTypeLookupId = c.Int(),
                        OptionTypeLookupId = c.Int(),
                        PayoutTypeLookupId = c.Int(),
                    })
                .PrimaryKey(t => t.FieldLookupId);
            
            CreateTable(
                "dbo.FieldValues",
                c => new
                    {
                        FieldValueId = c.Int(nullable: false, identity: true),
                        FieldLookupId = c.Int(nullable: false),
                        Value = c.String(),
                        IsScrubbed = c.Boolean(nullable: false),
                        CaId = c.Int(),
                        OptionId = c.Int(),
                        PayoutId = c.Int(),
                    })
                .PrimaryKey(t => t.FieldValueId)
                .ForeignKey("dbo.FieldLookups", t => t.FieldLookupId, cascadeDelete: true)
                .Index(t => t.FieldLookupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FieldValues", "FieldLookupId", "dbo.FieldLookups");
            DropIndex("dbo.FieldValues", new[] { "FieldLookupId" });
            DropTable("dbo.FieldValues");
            DropTable("dbo.FieldLookups");
        }
    }
}
