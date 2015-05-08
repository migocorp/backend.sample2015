[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed")]

namespace Sample2015.Web.Models.Api.Account
{
    using System.Collections.Generic;
    using System.Linq;
    using Nancy;
    using Newtonsoft.Json;
    using Sample2015.Core.Model.EF;

    public class RspAccountUserList : RspFrame
    {
        public RspAccountUserList()
            : base()
        {
        }

        public RspAccountUserList(HttpStatusCode codeHttp, IEnumerable<AccountUser> users)
            : base(codeHttp)
        {
            this.Result = users == null ? new List<AccountUser>() : users.ToList();
        }

        public List<AccountUser> Result { get; set; }
    }

    public class RspAccountUserResult
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string User_name { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string User_username { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string User_email { get; set; }

        //// [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        //// public IList<RspAccountUserCompany> company { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string User_language { get; set; }
    }

    public class RspAccountUserCompany
    {
        public string Company_name { get; set; }

        public int Company_id { get; set; }
    }
}