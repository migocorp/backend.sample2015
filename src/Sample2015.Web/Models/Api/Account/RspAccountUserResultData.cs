[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]

namespace Sample2015.Web.Models.Api.Account
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Newtonsoft.Json;
    using Sample2015.Core.Model.EF;

    public class RspAccountUserResultData
    {
        public RspAccountUserResultData()
        {
        }

        public RspAccountUserResultData(AccountUser user)
        {
            if (user == null)
            {
                return;
            }

            this.user_username = user.Username;
            this.user_email = user.Email;
            this.user_name = user.Name;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string user_username { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string user_email { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string user_name { get; set; }
    }
}