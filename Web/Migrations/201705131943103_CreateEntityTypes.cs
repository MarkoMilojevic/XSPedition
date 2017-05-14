namespace Web.Migrations
{
	using System.Data.Entity.Migrations;
    
    public partial class CreateEntityTypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CaTypes",
                c => new
                    {
                        CaTypeId = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CaTypeId);
            
            CreateTable(
                "dbo.CorporateActions",
                c => new
                    {
                        CaId = c.Int(nullable: false, identity: true),
                        CaTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CaId)
                .ForeignKey("dbo.CaTypes", t => t.CaTypeId, cascadeDelete: true)
                .Index(t => t.CaTypeId);
            
            CreateTable(
                "dbo.OptionTypes",
                c => new
                    {
                        OptionTypeId = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.OptionTypeId);
            
            CreateTable(
                "dbo.PayoutTypes",
                c => new
                    {
                        PayoutTypeId = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PayoutTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CorporateActions", "CaTypeId", "dbo.CaTypes");
            DropIndex("dbo.CorporateActions", new[] { "CaTypeId" });
            DropTable("dbo.PayoutTypes");
            DropTable("dbo.OptionTypes");
            DropTable("dbo.CorporateActions");
            DropTable("dbo.CaTypes");
        }
    }
}
