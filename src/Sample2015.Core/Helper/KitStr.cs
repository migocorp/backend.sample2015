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
    }
}
