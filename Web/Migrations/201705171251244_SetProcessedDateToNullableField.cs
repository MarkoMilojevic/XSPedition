namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetProcessedDateToNullableField : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NotificationInfo", "ProcessedDate", c => c.DateTime());
            AlterColumn("dbo.ScrubbingInfo", "ProcessedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ScrubbingInfo", "ProcessedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.NotificationInfo", "ProcessedDate", c => c.DateTime(nullable: false));
        }
    }
}
