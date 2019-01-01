namespace MCPart3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCategory : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Accessories", "Name");
            AddForeignKey("dbo.Accessories", "Name", "dbo.Categories", "Name", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accessories", "Name", "dbo.Categories");
            DropIndex("dbo.Accessories", new[] { "Name" });
        }
    }
}
