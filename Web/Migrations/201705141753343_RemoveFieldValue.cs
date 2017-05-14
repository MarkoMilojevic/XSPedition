namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveFieldValue : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.FieldValues", "Value");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FieldValues", "Value", c => c.String(nullable: false));
        }
    }
}
