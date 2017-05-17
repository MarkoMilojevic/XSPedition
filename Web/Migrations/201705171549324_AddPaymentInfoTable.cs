namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPaymentInfoTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentInfo",
                c => new
                    {
                        PaymentInfoId = c.Int(nullable: false, identity: true),
                        CaId = c.Int(nullable: false),
                        CaTypeId = c.Int(),
                        VolManCho = c.String(),
                        FieldDisplay = c.String(),
                        AccountNumber = c.String(),
                        OptionNumber = c.Int(nullable: false),
                        PayoutNumber = c.Int(nullable: false),
                        ProcessedDate = c.DateTime(),
                        ProcessedDateCategory = c.Int(nullable: false),
                        IsSettled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentInfoId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PaymentInfo");
        }
    }
}
