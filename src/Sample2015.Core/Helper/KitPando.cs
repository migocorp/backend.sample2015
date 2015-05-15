[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1310:FieldNamesMustNotContainUnderscore", Justification = "Reviewed")]

namespace Sample2015.Core.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class KitPando
    {
        public static readonly int PANDO_NA_VALUE = -999;

        public static int? ToInt(object val)
        {
            if (val == null)
            {
                return null;
            }

            var type = val.GetType();
            if (val.GetType() == typeof(int))
            {
                return ToIntFromInt((int)val);
            }

            if (val.GetType() == typeof(double))
            {
                return ToIntFromDouble((double)val);
            }
            else if (val.GetType() == typeof(string))
            {
                return ToIntFromStr((string)val);
            }

            return null;
        }

        public static int? ToIntFromDouble(double? val)
        {
            if (val == null || val <= PANDO_NA_VALUE)
            {
                return null;
            }

            return (int)val;
        }

        public static int? ToIntFromInt(int? val)
        {
            if (val == null || val <= PANDO_NA_VALUE)
            {
                return null;
            }

            return (int)val;
        }

        public static int? ToIntFromStr(string val)
        {
            var valueInt = KitStr.ParseInt(val, null);
            if (valueInt == null || valueInt <= PANDO_NA_VALUE)
            {
                return null;
            }

            return valueInt;
        }

        public static double? ToDouble(object val)
        {
            if (val == null)
            {
                return null;
            }

            var typeVal = val.GetType();
            if (val.GetType() == typeof(int))
            {
                return (int)val <= PANDO_NA_VALUE ? null : (double?)val;
            }
            else if (val.GetType() == typeof(double))
            {
                var doubleVal = (double)val;
                return doubleVal <= PANDO_NA_VALUE ? null : (double?)doubleVal;
            }
            else if (val.GetType() == typeof(string))
            {
                return ToDoubleFromStr((string)val);
            }

            return null;
        }

        public static double? ToDoubleFromStr(string val)
        {
            if (val == null)
            {
                return null;
            }

            var valueDouble = KitStr.ParseDouble(val, null);
            if (valueDouble == null || valueDouble <= PANDO_NA_VALUE)
            {
                return null;
            }

            return valueDouble;
        }

        public static bool? ToBool(object val)
        {
            if (val == null)
            {
                return null;
            }

            if (val.GetType() == typeof(int))
            {
                var valInt = ToInt(val);
                if (valInt == null)
                {
                    return null;
                }

                switch (valInt)
                {
                    case 0:
                        return false;
                    case 1:
                        return true;
                }

                return null;
            }
            else if (val.GetType() == typeof(string))
            {
                return ToBoolFromStr((string)val);
            }

            return null;
        }

        public static bool? ToBoolFromStr(string val)
        {
            if (val == null || val.StartsWith("-99"))
            {
                return null;
            }

            var valBool = KitStr.ParseBool(val, null);
            return valBool;
        }
    }
}
