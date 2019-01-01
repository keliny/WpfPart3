namespace MCPart3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeOfCategory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Accessories", "Name", "dbo.Categories");
            DropIndex("dbo.Accessories", new[] { "Name" });
            DropPrimaryKey("dbo.Categories");
            AddColumn("dbo.Accessories", "Category_CategoryName", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Categories", "CategoryName", c => c.String(nullable: false, maxLength: 100));
            AddPrimaryKey("dbo.Categories", "CategoryName");
            CreateIndex("dbo.Accessories", "Category_CategoryName");
            AddForeignKey("dbo.Accessories", "Category_CategoryName", "dbo.Categories", "CategoryName", cascadeDelete: true);
            DropColumn("dbo.Categories", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 100));
            DropForeignKey("dbo.Accessories", "Category_CategoryName", "dbo.Categories");
            DropIndex("dbo.Accessories", new[] { "Category_CategoryName" });
            DropPrimaryKey("dbo.Categories");
            DropColumn("dbo.Categories", "CategoryName");
            DropColumn("dbo.Accessories", "Category_CategoryName");
            AddPrimaryKey("dbo.Categories", "Name");
            CreateIndex("dbo.Accessories", "Name");
            AddForeignKey("dbo.Accessories", "Name", "dbo.Categories", "Name", cascadeDelete: true);
        }
    }
}
