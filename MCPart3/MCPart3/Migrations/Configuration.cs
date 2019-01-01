using MCPart3.Models;

namespace MCPart3.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<MCPart3.MCDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MCPart3.MCDB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Categories.AddOrUpdate(x => x.CategoryName,
                new Category() { CategoryName = "Office supplies" },
                new Category() { CategoryName = "Drinks" },
                new Category() { CategoryName = "Food" },
                new Category() { CategoryName = "Inventory" },
                new Category() { CategoryName = "Other" }
                );
        }
    }
}
