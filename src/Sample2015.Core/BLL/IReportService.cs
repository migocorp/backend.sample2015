namespace Sample2015.Core.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Sample2015.Core.Helper.Enums;

    public interface IReportService<T>
    {
        void InsertBulkReports(IEnumerable<T> newDocs);

        List<T> FindReportsByDate(DateTime date, RptDef.Period period);

        void DropCollection();
    }
}
