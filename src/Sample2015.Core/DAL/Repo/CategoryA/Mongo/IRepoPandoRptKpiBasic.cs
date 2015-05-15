namespace Sample2015.Core.DAL.Repo.CategoryA.Mongo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Sample2015.Core.Helper.Enums;
    using Sample2015.Core.Model.Mongo.Pandora;

    public interface IRepoPandoRptKpiBasic
    {
        Task<List<PandoRptKpiBasic>> FindRptsByDate(DateTime date, RptDef.Period period);
    }
}
