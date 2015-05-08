namespace Sample2015.Core.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Sample2015.Core.Model.EF;

    public class DataInitializer : CreateDatabaseIfNotExists<CoreDbContext>
    {
        protected override void Seed(CoreDbContext context)
        {
            var userSys = new AccountUser { Username = "admin", Password = "Admin1234", Email = "admin@migo.com", Name = "admin" };
            var userSys2 = new AccountUser { Username = "Test", Password = "Test1234", Email = "test@migo.com", Name = "test" };

            context.AccountUsers.Add(userSys);
            context.AccountUsers.Add(userSys2);

            context.SaveChanges();
        }
    }
}
