namespace Sample2015.Core.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Sample2015.Core.Model.EF;

    public class CoreDbContext : DbContext
    {
        public CoreDbContext() : base("core-db-context")
        {
            Database.SetInitializer<CoreDbContext>(new DataInitializer());
        }

        public DbSet<AccountUser> AccountUsers { get; set; }
    }
}
