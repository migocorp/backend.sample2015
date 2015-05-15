namespace Sample2015.Core.DAL.Repo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IMongodbRepositoryRW<T>
    {
        string GetDbName();

        string GetCollectionName();

        bool CreateCollectionIfNotExist();

        Task InsertBulkAsync(IEnumerable<T> newDocs);

        void InsertBulk(IEnumerable<T> allDocs, int batchSize = 500);

        bool CollectionExists(string nameCol);

        void DropCollection(string nameCol);
    }
}
