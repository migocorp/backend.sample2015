namespace Sample2015.Web.Models.Api.Account
{
    using Nancy;
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
            this.Result = u == null ? new RspAccountUserResult() :
                new RspAccountUserResult
                {
                    ////company = new List<RspAccountUserCompany> { 
                    ////    new RspAccountUserCompany 
                    ////    { 
                    ////        company_name = u.AccountCompany.Name,
                    ////        company_id = u.AccountCompany.ID
                    ////    } 
                    ////},
                    User_name = u.Name,
                    User_username = u.Username,
                    User_email = u.Email
                    ////user_language = u.Language
                };
        }

        public RspAccountUserResult Result { get; set; }
    }
}