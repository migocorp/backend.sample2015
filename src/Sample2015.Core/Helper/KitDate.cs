namespace Sample2015.Core.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Sample2015.Core.Helper.Enums;

    public class KitDate
    {
        public static DateTime GetDatePandora(RptDef.Period period, DateTime d)
        {
            return period == RptDef.Period.Weekly ? GetAddedDate(d, 0) : GetCurrentMonthLastDay(d);
        }

        public static DateTime GetAddedDate(DateTime? d, int days)
        {
            DateTime dn = d == null ? DateTime.Now : (DateTime)d;
            dn = dn.AddDays(days);
            return new DateTime(dn.Year, dn.Month, dn.Day);
        }

        public static DateTime GetCurrentMonthLastDay(DateTime d)
        {
            return new DateTime(d.Year, d.Month, DateTime.DaysInMonth(d.Year, d.Month));
        }

        public static DateTime GetCurrentMonthFirstDay(DateTime d)
        {
            return new DateTime(d.Year, d.Month, 1);
        }
    }
}
