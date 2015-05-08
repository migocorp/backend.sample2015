namespace Sample2015.Web.Models.Api.Account
{
    using Nancy;
    using Newtonsoft.Json;
    using Sample2015.Core.Model.EF;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class RspAccountUserList : RspFrame
    {
        public RspAccountUserList()
            : base()
        {
        }

        public RspAccountUserList(HttpStatusCode codeHttp, IEnumerable<AccountUser> users)
            : base(codeHttp)
        {
            this.result = users == null ? new List<AccountUser>() : users.ToList();
        }

        public List<AccountUser> result { get; set; }
    }

    public class RspAccountUserResult
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string user_name { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string user_username { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string user_email { get; set; }

        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        //public IList<RspAccountUserCompany> company { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string user_language { get; set; }
    }

    public class RspAccountUserCompany
    {
        public string company_name { get; set; }

        public int company_id { get; set; }
    }
}