namespace MCPart3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateHandOutsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HandOuts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        CustomerName = c.String(nullable: false, maxLength: 100),
                        Accessory_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accessories", t => t.Accessory_Id, cascadeDelete: true)
                .Index(t => t.Accessory_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HandOuts", "Accessory_Id", "dbo.Accessories");
            DropIndex("dbo.HandOuts", new[] { "Accessory_Id" });
            DropTable("dbo.HandOuts");
        }
    }
}
