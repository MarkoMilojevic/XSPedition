namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NeZnamStaSamRadio : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FieldValues", "Value", c => c.String());
            AlterColumn("dbo.Options", "OptionNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.Payouts", "PayoutNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Payouts", "PayoutNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Options", "OptionNumber", c => c.String(nullable: false));
            DropColumn("dbo.FieldValues", "Value");
        }
    }
}
