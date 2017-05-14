namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldLookupIdToScrubbingView : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScrubbingProcessViews", "FieldLookupId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ScrubbingProcessViews", "FieldLookupId");
        }
    }
}
