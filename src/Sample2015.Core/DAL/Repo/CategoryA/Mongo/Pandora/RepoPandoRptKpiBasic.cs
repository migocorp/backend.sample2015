namespace Sample2015.Core.DAL.Repo.CategoryA.Mongo.Pandora
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using MongoDB.Driver.Linq;
    using Sample2015.Core.DAL.DbContextScope;
    using Sample2015.Core.Helper;
    using Sample2015.Core.Helper.Enums;
    using Sample2015.Core.Model.Mongo.Pandora;

    public class RepoPandoRptKpiBasic : MongodbRepository<PandoRptKpiBasic>, IRepoPandoRptKpiBasic
    {
        public RepoPandoRptKpiBasic(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator, KitConfig.GetDbMongoUriPandora())
        {
        }

        public override string GetDbName()
        {
            return KitConfig.GetDbNamePandoraReport();
        }

        public override string GetCollectionName()
        {
            return "DIYReport";
        }

        public async Task<List<PandoRptKpiBasic>> FindRptsByDate(DateTime date, RptDef.Period period)
        {
            var col = this.GetCollection();
            DateTime dateQuery = KitDate.GetDatePandora(period, date);
            string periodQuery = RptDef.ToPandoraKey(period, dateQuery);

            List<PandoRptKpiBasic> rpts = await
                col.Find(r => r.ShopID == "ablejeans" && r.RptDate == dateQuery && r.PeriodType == periodQuery)
                .ToListAsync().ConfigureAwait(continueOnCapturedContext: false);
            return rpts;
        }
    }
}
