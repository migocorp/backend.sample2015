[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]

namespace Sample2015.Web.Models.Api.Account
{
    using Nancy;
    using Newtonsoft.Json;
    using Sample2015.Core.Model.EF;

    public class RspAccountUser : RspFrame
    {
        public RspAccountUser()
            : base()
        {
        }

        public RspAccountUser(HttpStatusCode codeHttp, AccountUser u = null)
            : base(codeHttp)
        {
            this.result = new RspAccountUserResultData();
        }

        public RspAccountUserResultData result { get; set; }
    }
}