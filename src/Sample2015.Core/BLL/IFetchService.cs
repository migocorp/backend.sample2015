namespace Sample2015.Core.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Sample2015.Core.Helper.Enums;
    using Sample2015.Core.Model.Mongo.Pandora;

    public interface IFetchService
    {
        List<PandoRptKpiBasic> FetchReportStoreKpiFromBiPandora(DateTime date, RptDef.Period period);
    }
}
