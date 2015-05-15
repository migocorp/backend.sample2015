[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Using Win32 naming for consistency.")]

namespace Sample2015.Core.DAL.DbContextScope
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MongoDB.Driver;

    /// <summary>
    /// Handle class for holding the real DbContext and some state for it.
    /// </summary>
    public class DbContextCollection : IDisposable
    {
        private Dictionary<Type, DbContext> initializedDbContexts;
        private Dictionary<string, MongoClient> initializedMongoClients;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextCollection" /> class.
        /// </summary>
        /// <param name="forWriting">Is the root context opened for writing?</param>
        public DbContextCollection(bool forWriting)
        {
            this.ForWriting = forWriting;

            this.initializedDbContexts = new Dictionary<Type, DbContext>();
            this.initializedMongoClients = new Dictionary<string, MongoClient>();
        }

        /// <summary>
        /// Gets a value indicating whether Was any unit of work scope using this DbContext opened for writing?
        /// </summary>
        public bool ForWriting { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether Has there been a failure that should block saving?
        /// </summary>
        public bool BlockSave { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Switch off guard for direct calls to SaveChanges.
        /// </summary>
        public bool AllowSaving { get; set; }

        public TDbContext Get<TDbContext>() where TDbContext : DbContext
        {
            var requestedType = typeof(TDbContext);

            if (!this.initializedDbContexts.ContainsKey(requestedType))
            {
                var dbContext = Activator.CreateInstance<TDbContext>();
                this.initializedDbContexts.Add(requestedType, dbContext);
                ((IObjectContextAdapter)dbContext).ObjectContext.SavingChanges += this.GuardAgainstDirectSaves;
            }

            return this.initializedDbContexts[requestedType] as TDbContext;
        }

        public TMongoClient GetMongoDb<TMongoClient>(string connectionString) where TMongoClient : MongoClient
        {
            if (!this.initializedMongoClients.ContainsKey(connectionString))
            {
                var dbContext = (TMongoClient)Activator.CreateInstance(typeof(TMongoClient), connectionString);
                this.initializedMongoClients.Add(connectionString, dbContext);
            }

            return this.initializedMongoClients[connectionString] as TMongoClient;
        }

        public void SaveChanges()
        {
            foreach (var dbContext in this.initializedDbContexts.Values)
            {
                dbContext.SaveChanges();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var dbContext in this.initializedDbContexts.Values)
                {
                    try
                    {
                        ((IObjectContextAdapter)dbContext).ObjectContext.SavingChanges -= this.GuardAgainstDirectSaves;
                        dbContext.Dispose();
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(e);
                    }
                }

                this.initializedDbContexts.Clear();
                this.initializedMongoClients.Clear();
            }
        }

        private void GuardAgainstDirectSaves(object sender, EventArgs e)
        {
            if (!this.AllowSaving)
            {
                throw new InvalidOperationException(
                    "Don't call SaveChanges directly on a context owned by a UnitOfWorkScope. " +
                    "use UnitOfWorkScope.SaveChanges instead.");
            }
        }
    }
}
