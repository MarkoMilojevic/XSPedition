namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInstructionInfoTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InstructionInfo",
                c => new
                    {
                        InstructionInfoId = c.Int(nullable: false, identity: true),
                        CaId = c.Int(nullable: false),
                        CaTypeId = c.Int(),
                        VolManCho = c.String(),
                        FieldDisplay = c.String(),
                        AccountNumber = c.String(),
                        ProcessedDate = c.DateTime(),
                        ProcessedDateCategory = c.Int(nullable: false),
                        IsInstructed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.InstructionInfoId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InstructionInfo");
        }
    }
}
