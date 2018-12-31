namespace MCPart3
{
    using System.Data.Entity;

    public partial class MCDB : DbContext
    {
        public MCDB()
            : base("name=MCDB")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
