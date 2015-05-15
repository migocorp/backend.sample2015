namespace Sample2015.Test.Sample
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Nancy.TinyIoc;
    using Sample2015.Core.BLL;
    using Sample2015.Core.BLL.Simple;
    using Sample2015.Core.Helper;
    using Sample2015.Core.Helper.Enums;
    using Sample2015.Core.Model.Mongo;

    public class ExampleTest
    {
        public static void Main()
        {
            var ctx = TinyIoCContainer.Current;
            ctx.AutoRegister();

            var fetchService = ctx.Resolve<IFetchService>();
            DateTime testDate = DateTime.ParseExact("2015-01-01", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            var pandoRpts = fetchService.FetchReportStoreKpiFromBiPandora(testDate, RptDef.Period.Weekly);

            Console.WriteLine("AbleJeans at Pandora on 2015-01-01 Weekly Kpi has " + pandoRpts.Count + " reports.");
            Console.ReadKey();

            var lst = new List<RptStoreKpiBasic>();
            foreach (var pandoRpt in pandoRpts)
            {
                lst.Add(new RptStoreKpiBasic()
                    {
                        Period = RptDef.ToDbKey(RptDef.Period.Weekly),
                        DateReport = testDate,
                        MemberCountN0 = KitPando.ToInt(pandoRpt.Records.MemberCounts[0]),
                        MemberCountE0 = KitPando.ToInt(pandoRpt.Records.MemberCounts[1]),
                        MemberCountS1 = KitPando.ToInt(pandoRpt.Records.MemberCounts[2]),
                        MemberCountS2 = KitPando.ToInt(pandoRpt.Records.MemberCounts[3]),
                        MemberCountS3 = KitPando.ToInt(pandoRpt.Records.MemberCounts[4])
                    });
            }

            ctx.Register(typeof(IReportService<>), typeof(RptStoreKpiBasicService));
            var rptService = ctx.Resolve<IReportService<RptStoreKpiBasic>>();
            rptService.InsertBulkReports(lst);
            var lstRpts = rptService.FindReportsByDate(testDate, RptDef.Period.Weekly);

            Console.WriteLine("AbleJeans at local on 2015-01-01 Weekly Kpi has " + lstRpts.Count + " reports.");
            Console.ReadKey();

            rptService.DropCollection();
        }
    }
}
