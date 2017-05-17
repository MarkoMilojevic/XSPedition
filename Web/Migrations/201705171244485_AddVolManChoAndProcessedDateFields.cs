namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVolManChoAndProcessedDateFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NotificationInfo", "VolManCho", c => c.String());
            AddColumn("dbo.ScrubbingInfo", "VolManCho", c => c.String());
            AddColumn("dbo.ScrubbingInfo", "ProcessedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ScrubbingInfo", "ProcessedDate");
            DropColumn("dbo.ScrubbingInfo", "VolManCho");
            DropColumn("dbo.NotificationInfo", "VolManCho");
        }
    }
}
