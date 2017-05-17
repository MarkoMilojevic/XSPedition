namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotificationsInfoTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NotificationInfo",
                c => new
                    {
                        NotificationInfoId = c.Int(nullable: false, identity: true),
                        CaId = c.Int(nullable: false),
                        CaTypeId = c.Int(),
                        FieldDisplay = c.String(),
                        AccountNumber = c.String(),
                        Recipient = c.String(),
                        ProcessedDate = c.DateTime(nullable: false),
                        ProcessedDateCategory = c.Int(nullable: false),
                        IsSent = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.NotificationInfoId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NotificationInfo");
        }
    }
}
