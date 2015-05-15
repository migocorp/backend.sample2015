namespace Sample2015.Core.DAL.Repo.CategoryA.Mongo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using Sample2015.Core.DAL.DbContextScope;
    using Sample2015.Core.Helper;
    using Sample2015.Core.Helper.Enums;
    using Sample2015.Core.Model.Mongo;

    public class RepoRptStoreKpiBasic : MongodbRepository<RptStoreKpiBasic>, IRepoRptStoreKpiBasic
    {
        public RepoRptStoreKpiBasic(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator, KitConfig.GetDbMongoConnectString())
        {
        }

        public override string GetDbName()
        {
            return KitConfig.GetMongoDbCoreName();
        }

        public override string GetCollectionName()
        {
            return typeof(RptStoreKpiBasic).Name;
        }

        public async Task<List<RptStoreKpiBasic>> FindRptsByDate(DateTime date, RptDef.Period period)
        {
            var col = this.GetCollection();
            DateTime dateQuery = KitDate.GetDatePandora(period, date);

            List<RptStoreKpiBasic> rpts = await
                col.Find(r => r.DateReport == dateQuery && r.Period == RptDef.ToDbKey(period))
                .ToListAsync().ConfigureAwait(continueOnCapturedContext: false);
            return rpts;
        }

        public void DropCollection()
        {
            this.DropCollection(this.GetCollectionName());
        }
    }
}
