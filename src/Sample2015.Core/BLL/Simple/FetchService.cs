namespace Sample2015.Core.BLL.Simple
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Sample2015.Core.DAL.DbContextScope;
    using Sample2015.Core.DAL.Repo.CategoryA.Mongo;
    using Sample2015.Core.Helper.Enums;
    using Sample2015.Core.Model.Mongo.Pandora;

    public class FetchService : BaseService, IFetchService
    {
        private readonly IRepoPandoRptKpiBasic repoPandoRptKpiBasic;

        public FetchService(IRepoPandoRptKpiBasic repoPandoRptKpiBasic)
            : base()
        {
            this.repoPandoRptKpiBasic = repoPandoRptKpiBasic;
        }

        public List<PandoRptKpiBasic> FetchReportStoreKpiFromBiPandora(DateTime date, RptDef.Period period)
        {
            return this.repoPandoRptKpiBasic.FindRptsByDate(date, period).Result;
        }
    }
}
