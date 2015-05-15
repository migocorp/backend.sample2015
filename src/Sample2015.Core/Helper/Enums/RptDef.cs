namespace Sample2015.Core.Helper.Enums
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RptDef
    {
        public static readonly string DbPeriodWeekly = "W";
        public static readonly string DbPeriodMonthly = "M";

        public static readonly string DbTypeLrfmL = "L";
        public static readonly string DbTypeLrfmR = "R";
        public static readonly string DbTypeLrfmF = "F";
        public static readonly string DbTypeLrfmM = "M";

        public static readonly string DbTypeNesN0 = "N0";
        public static readonly string DbTypeNesE0 = "E0";
        public static readonly string DbTypeNesS1 = "S1";
        public static readonly string DbTypeNesS2 = "S2";
        public static readonly string DbTypeNesS3 = "S3";

        public enum TypeHotItemRankMeasure : byte
        {
            SaleAmount = 0,
            Quantity
        }

        public enum TypeKpi : byte
        {
            RatePlusN0 = 0,
            RatePlusE0,
            RatePlusS3,
            RateStickN0,
            RateActiveE0,
            RateWakeS1,
            RateWakeS2,
            RateWakeS3,
            ArpuN0,
            ArpuE0
        }

        public enum Period
        {
            Weekly = 0,
            Monthly,
            Undefined
        }

        public enum TypeNes
        {
            N0 = 0, E0, S1, S2, S3
        }

        public enum TypeStoreDimension
        {
            MemberCount = 0, RatePlus, RateOther, Arpu
        }

        public enum TypeLrfm
        {
            Length = 0, Recency, Frequency, Monetary
        }

        public static byte ToDbKey(TypeKpi t)
        {
            return (byte)t;
        }

        public static string ToDbKey(TypeNes t)
        {
            switch (t)
            {
                case TypeNes.N0:
                    return DbTypeNesN0;
                case TypeNes.E0:
                    return DbTypeNesE0;
                case TypeNes.S1:
                    return DbTypeNesS1;
                case TypeNes.S2:
                    return DbTypeNesS2;
                case TypeNes.S3:
                    return DbTypeNesS3;
            }

            throw new ArgumentException("unrecognized TypeNes.");
        }

        public static byte ToDbKey(TypeHotItemRankMeasure rankMeasure)
        {
            return (byte)rankMeasure;
        }

        public static string ToDbKey(Period period)
        {
            switch (period)
            {
                case Period.Weekly:
                    return DbPeriodWeekly;
                case Period.Monthly:
                    return DbPeriodMonthly;
            }

            throw new ArgumentException("unrecognized RptPeriod.");
        }

        public static string ToDbKey(TypeLrfm t)
        {
            switch (t)
            {
                case TypeLrfm.Length:
                    return DbTypeLrfmL;
                case TypeLrfm.Recency:
                    return DbTypeLrfmR;
                case TypeLrfm.Frequency:
                    return DbTypeLrfmF;
                case TypeLrfm.Monetary:
                    return DbTypeLrfmM;
            }

            throw new ArgumentException("unrecognized TypeLrfm.");
        }

        public static TypeNes FromDbKeyToTypeNes(string key)
        {
            if (KitStr.IsEqual(key, DbTypeNesN0))
            {
                return TypeNes.N0;
            }
            else if (KitStr.IsEqual(key, DbTypeNesE0))
            {
                return TypeNes.E0;
            }
            else if (KitStr.IsEqual(key, DbTypeNesS1))
            {
                return TypeNes.S1;
            }
            else if (KitStr.IsEqual(key, DbTypeNesS2))
            {
                return TypeNes.S2;
            }

            return TypeNes.S3;
        }

        public static TypeLrfm FromDbKeyToTypeLrfm(string key)
        {
            if (KitStr.IsEqual(key, DbTypeLrfmL))
            {
                return TypeLrfm.Length;
            }
            else if (KitStr.IsEqual(key, DbTypeLrfmR))
            {
                return TypeLrfm.Recency;
            }
            else if (KitStr.IsEqual(key, DbTypeLrfmF))
            {
                return TypeLrfm.Frequency;
            }

            return TypeLrfm.Monetary;
        }

        public static Period FromDbKeyToPeriod(string keyPeriod)
        {
            if (keyPeriod.Equals(DbPeriodWeekly, StringComparison.InvariantCultureIgnoreCase))
            {
                return Period.Weekly;
            }
            else if (keyPeriod.Equals(DbPeriodMonthly, StringComparison.InvariantCultureIgnoreCase))
            {
                return Period.Monthly;
            }

            return Period.Undefined;
        }

        public static DateTime? GetFrontendApiDataStartDate(DateTime? date, Period period)
        {
            if (date == null || ((DateTime)date).Year < 1901)
            {
                return null;
            }

            DateTime d = (DateTime)date;
            return period == Period.Weekly ? d : KitDate.GetCurrentMonthFirstDay(d);
        }

        public static DateTime? GetFrontendApiStartDate(DateTime? date, Period period)
        {
            if (date == null || ((DateTime)date).Year < 1901)
            {
                return null;
            }

            DateTime d = (DateTime)date;
            return period == Period.Weekly ? d.AddDays(-6) : KitDate.GetCurrentMonthFirstDay(d);
        }

        public static DateTime? GetFrontendApiStartDate(DateTime date, Period period)
        {
            return GetFrontendApiStartDate((DateTime?)date, period);
        }

        public static DateTime? GetFrontendApiEndDate(DateTime? date, Period period)
        {
            if (date == null || ((DateTime)date).Year < 1901)
            {
                return null;
            }

            DateTime d = (DateTime)date;
            return period == Period.Weekly ? d : KitDate.GetCurrentMonthLastDay(d);
        }

        public static DateTime? GetFrontendApiEndDate(DateTime date, Period period)
        {
            return GetFrontendApiEndDate((DateTime?)date, period);
        }

        public static Period FromPandoraKey(string keyPandora)
        {
            if (keyPandora.StartsWith("L7", StringComparison.InvariantCultureIgnoreCase))
            {
                return Period.Weekly;
            }

            return Period.Monthly;
        }

        public static string ToPandoraKey(Period period, DateTime date)
        {
            switch (period)
            {
                case Period.Weekly:
                    return "L7D";
                case Period.Monthly:
                    int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
                    return string.Format("L{0}D", daysInMonth);
            }

            throw new ArgumentException("unrecognized RptPeriod.");
        }

        public static int? AvoidPandoraNaN(int? val)
        {
            return val == null || val <= -900 ? null : val;
        }

        public static decimal? AvoidPandoraNaN(decimal? val)
        {
            return val == null || val <= -900.0m ? null : val;
        }

        public static double? AvoidPandoraNaN(double? val)
        {
            return val == null || val <= -900.0 ? null : val;
        }
    }
}
