using MCPart3.Models;

namespace MCPart3
{
    using System.Data.Entity;

    public class MCDB : DbContext
    {
        public MCDB()
            : base("name=MCDB")
        {
        }

        public DbSet<Accessory> Accessories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<HandOut> HandOuts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }


    }
}
