[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed")]
[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]

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
            this.result = new RspAccountUserResult();
            if (users == null)
            {
                return;
            }

            this.result.count = users.Count();
            users.ToList().ForEach(u => this.result.data.Add(new RspAccountUserResultData(u)));
        }

        public RspAccountUserResult result { get; set; }
    }

    public class RspAccountUserResult
    {
        public RspAccountUserResult()
        {
            this.count = 0;
            this.data = new List<RspAccountUserResultData>();
        }

        public int count { get; set; }

        public IList<RspAccountUserResultData> data { get; set; }
    }

    public class RspAccountUserResultData
    {
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