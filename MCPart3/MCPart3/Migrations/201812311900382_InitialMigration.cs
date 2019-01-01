namespace MCPart3.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accessories",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 100),
                    Min = c.Int(nullable: false),
                    AmountStored = c.Int(nullable: false),
                    Status = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Categories",
                c => new
                {
                    Name = c.String(nullable: false, maxLength: 100),
                })
                .PrimaryKey(t => t.Name);

        }

        public override void Down()
        {
            DropTable("dbo.Categories");
            DropTable("dbo.Accessories");
        }
    }
}
