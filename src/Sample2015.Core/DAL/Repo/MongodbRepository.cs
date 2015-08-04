namespace Sample2015.Core.DAL.Repo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using Sample2015.Core.DAL.DbContextScope;

    public abstract class MongodbRepository<T>
    {
        private readonly string connectionString;

        public MongodbRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public virtual MongoClient Client
        {
            get
            {
                var client = new MongoClient(this.connectionString);
                if (client == null)
                {
                    throw new InvalidOperationException("No ambient connectionString: " + this.connectionString + " of MongoClient found.");
                }

                return client;
            }
        }

        public abstract string GetDbName();

        public abstract string GetCollectionName();

        #region [Collection Operations]

        public bool CreateCollectionIfNotExist()
        {
            if (this.CollectionExists())
            {
                return false;
            }

            this.CreateCollection();
            return true;
        }

        public virtual void CreateCollection()
        {
            var collectName = this.GetCollectionName();
            this.GetDb().CreateCollectionAsync(collectName).Wait();
            this.SetupIndexes();
        }

        public bool CollectionExists()
        {
            return this.CollectionExists(this.GetCollectionName());
        }

        public bool CollectionExists(string colName)
        {
            return this.CollectionExists(this.GetDbName(), colName);
        }

        public bool CollectionExists(string nameDb, string nameCol)
        {
            if (string.IsNullOrEmpty(nameDb) || string.IsNullOrEmpty(nameCol))
            {
                return false;
            }

            var collections = this.GetDb(nameDb).ListCollectionsAsync().Result.ToListAsync().Result;
            foreach (var col in collections)
            {
                var name = col.GetValue("name");
                if (name != null)
                {
                    if (name.ToString().Equals(nameCol))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion

        #region [CRUD Operations]

        public virtual void InsertBulk<TModel>(IMongoCollection<TModel> col, IEnumerable<TModel> allDocs, int batchSize = 500)
        {
            List<TModel> newDocs = new List<TModel>();
            foreach (var d in allDocs)
            {
                newDocs.Add(d);
                if (newDocs.Count >= batchSize)
                {
                    var newInsertModels = new List<InsertOneModel<TModel>>();
                    newDocs.ForEach(doc => newInsertModels.Add(new InsertOneModel<TModel>(doc)));
                    col.BulkWriteAsync(newInsertModels).Wait();
                    newDocs.Clear();
                }
            }

            if (newDocs.Count > 0)
            {
                var newInsertModels = new List<InsertOneModel<TModel>>();
                newDocs.ForEach(d => newInsertModels.Add(new InsertOneModel<TModel>(d)));
                col.BulkWriteAsync(newInsertModels).Wait();
                newDocs.Clear();
            }
        }

        public virtual void InsertBulk(IEnumerable<T> allDocs, int batchSize = 500)
        {
            List<Task> tasks = new List<Task>();
            List<T> newDocs = new List<T>();
            foreach (var d in allDocs)
            {
                newDocs.Add(d);
                if (newDocs.Count >= batchSize)
                {
                    Task t = this.InsertBulkAsync(newDocs);
                    newDocs = new List<T>();
                    tasks.Add(t);
                }
            }

            if (newDocs.Count > 0)
            {
                Task t = this.InsertBulkAsync(newDocs);
                tasks.Add(t);
            }

            Task.WaitAll(tasks.ToArray());
        }

        public async virtual Task InsertBulkAsync<TModel>(IMongoCollection<TModel> col, IEnumerable<TModel> newDocs)
        {
            var newInsertModels = new List<InsertOneModel<TModel>>();
            foreach (var doc in newDocs)
            {
                newInsertModels.Add(new InsertOneModel<TModel>(doc));
            }

            await col.BulkWriteAsync(newInsertModels).ConfigureAwait(continueOnCapturedContext: false);
        }

        public async virtual Task InsertBulkAsync(IEnumerable<T> newDocs)
        {
            var col = this.GetCollection();
            
            await InsertBulkAsync<T>(col, newDocs).ConfigureAwait(continueOnCapturedContext: false);
        }
        #endregion

        public void DropCollection(string nameCol)
        {
            var db = this.GetDb();
            var taskDrop = db.DropCollectionAsync(nameCol);
            taskDrop.Wait();
        }

        public void DropCollection(string databaseName, string collectName)
        {
            var db = this.GetDb(databaseName);
            var taskDrop = db.DropCollectionAsync(collectName);
            taskDrop.Wait();
        }

        protected virtual string SetupIndexes()
        {
            return string.Empty;
        }

        protected IMongoCollection<T> GetCollection()
        {
            var db = this.GetDb();
            var collectName = this.GetCollectionName();
            return db.GetCollection<T>(collectName);
        }

        protected IMongoCollection<TModel> GetCollection<TModel>(string nameCol)
        {
            var db = this.GetDb();
            return db.GetCollection<TModel>(nameCol);
        }

        protected IMongoCollection<TModel> GetCollection<TModel>(string databaseName, string collectName)
        {
            var db = this.GetDb(databaseName);
            return db.GetCollection<TModel>(collectName);
        }

        protected IMongoDatabase GetDb(string databaseName)
        {
            return this.Client.GetDatabase(databaseName);
        }

        protected IMongoDatabase GetDb()
        {
            var databaseName = this.GetDbName();
            return this.GetDb(databaseName);
        }
    }
}
