namespace Sample2015.Core.BLL.Simple
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Sample2015.Core.DAL.DbContextScope;
    using Sample2015.Core.DAL.Repo.CategoryA;
    using Sample2015.Core.Helper.Enums;
    using Sample2015.Core.Model.Mongo;

    public class RptStoreKpiBasicService : BaseService, IReportService<RptStoreKpiBasic>
    {
        private readonly IRepoRptStoreKpiBasic repoRptStoreKpiBasic;

        public RptStoreKpiBasicService(IRepoRptStoreKpiBasic repoRptStoreKpiBasic)
            : base()
        {
            this.repoRptStoreKpiBasic = repoRptStoreKpiBasic;
        }

        public void InsertBulkReports(IEnumerable<RptStoreKpiBasic> newDocs)
        {
            using (var scope = new DbContextScope(DbContextScopePurpose.Writing))
            {
                this.repoRptStoreKpiBasic.InsertBulkAsync(newDocs).Wait();
            }
        }

        public List<RptStoreKpiBasic> FindReportsByDate(DateTime date, RptDef.Period period)
        {
            using (var scope = new DbContextScope(DbContextScopePurpose.Reading))
            {
                return this.repoRptStoreKpiBasic.FindRptsByDate(date, period).Result;
            }
        }

        public void DropCollection()
        {
            using (var scope = new DbContextScope(DbContextScopePurpose.Writing))
            {
                this.repoRptStoreKpiBasic.DropCollection();
            }
        }
    }
}
