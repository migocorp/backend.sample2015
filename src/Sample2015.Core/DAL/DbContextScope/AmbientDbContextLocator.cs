namespace Sample2015.Core.DAL.DbContextScope
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MongoDB.Driver;

    public class AmbientDbContextLocator : IAmbientDbContextLocator
    {
        public TDbContext Get<TDbContext>() where TDbContext : DbContext
        {
            var ambientScope = DbContextScope.GetAmbientScope();
            return ambientScope == null ? null : ambientScope.DbContext.Get<TDbContext>();
        }

        public TMongoClient GetMongoDb<TMongoClient>(string connectionString) where TMongoClient : MongoClient
        {
            var ambientScope = DbContextScope.GetAmbientScope();
            return ambientScope == null ? null : ambientScope.DbContext.GetMongoDb<TMongoClient>(connectionString);
        }
    }
}
