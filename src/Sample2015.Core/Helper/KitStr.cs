namespace Sample2015.Core.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class KitStr
    {
        public static T DeserializeObject<T>(string strJson, bool ignoreError = false)
        {
            if (!ignoreError)
            {
                return JsonConvert.DeserializeObject<T>(strJson);
            }

            return JsonConvert.DeserializeObject<T>(
                strJson,
                new JsonSerializerSettings
                {
                    Error = HandleDeserializationError
                });
        }

        public static string SerializeObject(object objJson)
        {
            return SerializeObject(objJson, false);
        }

        public static string SerializeObject(object objJson, bool withIndent)
        {
            return JsonConvert.SerializeObject(objJson, withIndent ? Formatting.Indented : Formatting.None);
        }

        public static void HandleDeserializationError(object sender, ErrorEventArgs errorArgs)
        {
            var currentError = errorArgs.ErrorContext.Error.Message;
            errorArgs.ErrorContext.Handled = true;
        }

        public static bool IsEqual(string str1, string str2)
        {
            return str1.Equals(str2, StringComparison.InvariantCultureIgnoreCase);
        }

        public static string FormatDate(DateTime? value)
        {
            return value == null ? string.Empty : string.Format("{0:yy-MM-dd}", value);
        }

        public static int? ParseInt(string str, int? valueFail)
        {
            int valueValid = 0;
            return (str != null && int.TryParse(str, out valueValid)) ? valueValid : valueFail;
        }

        public static double? ParseDouble(string str, double? valueFail)
        {
            double? val = valueFail;
            if (str != null)
            {
                double valParsed = 0.0;
                if (!double.TryParse(str, out valParsed))
                {
                    val = valueFail;
                }
                else
                {
                    val = valParsed;
                }
            }

            return val;
        }

        public static bool? ParseBool(string str, bool? valueFail)
        {
            bool? val = valueFail;
            if (str != null)
            {
                bool valToParse = false;
                if (!bool.TryParse(str, out valToParse))
                {
                    return null;
                }

                val = (bool?)valToParse;
            }

            return val;
        }
    }
}
