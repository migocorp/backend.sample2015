namespace Sample2015.Core.DAL.Repo.CategoryA
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Sample2015.Core.Helper.Enums;
    using Sample2015.Core.Model.Mongo;

    public interface IRepoRptStoreKpiBasic : IMongodbRepositoryRW<RptStoreKpiBasic>
    {
        Task<List<RptStoreKpiBasic>> FindRptsByDate(DateTime date, RptDef.Period period);

        void DropCollection();
    }
}
