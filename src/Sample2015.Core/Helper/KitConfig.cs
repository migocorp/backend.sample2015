[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1310:FieldNamesMustNotContainUnderscore", Justification = "Reviewed")]

namespace Sample2015.Core.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class KitConfig
    {
        public static readonly string KEY_DB_MONGO_CONNECT_STR = "db-mongo-connect-str";
        public static readonly string KEY_MONGO_DB_CORE_NAME = "mongo-db-core-name";
        public static readonly string KEY_DB_MONGO_URI_PANDORA = "db-mongo-uri-pandora";
        public static readonly string KEY_DB_MONGO_PANDORA_REPORT = "mongo-db-name-pandora-report";

        public static string GetDbMongoConnectString()
        {
            return AppKey(KEY_DB_MONGO_CONNECT_STR, false);
        }

        public static string GetMongoDbCoreName()
        {
            return GetAppKey(KEY_MONGO_DB_CORE_NAME, "sample2015_core");
        }

        public static string GetDbMongoUriPandora()
        {
            return AppKey(KEY_DB_MONGO_URI_PANDORA, false);
        }

        public static string GetDbNamePandoraReport()
        {
            return AppKey(KEY_DB_MONGO_PANDORA_REPORT, false);
        }

        public static string AppKey(string key, bool allowNotSetting)
        {
            string val = ConfigurationManager.AppSettings[key];
            if (!allowNotSetting && val == null)
            {
                throw new SettingsPropertyNotFoundException(string.Format("Cannot find key in config: {0}", key));
            }

            return val == null ? string.Empty : val;
        }

        public static string AppKey(string key)
        {
            return AppKey(key, true);
        }

        public static string GetAppKey(string key, string defaultStr)
        {
            string strValue = AppKey(key);
            if (string.IsNullOrEmpty(strValue))
            {
                return defaultStr;
            }

            return strValue;
        }
    }
}
