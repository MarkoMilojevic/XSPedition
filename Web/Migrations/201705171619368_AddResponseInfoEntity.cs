namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddResponseInfoEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ResponseInfo",
                c => new
                    {
                        ResponseInfoId = c.Int(nullable: false, identity: true),
                        CaId = c.Int(nullable: false),
                        CaTypeId = c.Int(),
                        VolManCho = c.String(),
                        OptionNumber = c.Int(),
                        AccountNumber = c.String(),
                        FieldDisplay = c.String(),
                        ProcessedDate = c.DateTime(),
                        ProcessedDateCategory = c.Int(nullable: false),
                        IsSubmitted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ResponseInfoId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ResponseInfo");
        }
    }
}
