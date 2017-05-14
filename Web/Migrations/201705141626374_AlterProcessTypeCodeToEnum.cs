namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterProcessTypeCodeToEnum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProcessTypeLookups", "Type", c => c.Int(nullable: false));
            DropColumn("dbo.ProcessTypeLookups", "Code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProcessTypeLookups", "Code", c => c.String(nullable: false));
            DropColumn("dbo.ProcessTypeLookups", "Type");
        }
    }
}
