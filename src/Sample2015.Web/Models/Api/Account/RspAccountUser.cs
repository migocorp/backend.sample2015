namespace Sample2015.Web.Models.Api.Account
{
    using Nancy;
    using Newtonsoft.Json;
    using Sample2015.Core.Model.EF;
    using System.Collections;
    using System.Collections.Generic;

    public class RspAccountUser : RspFrame
    {
        public RspAccountUser()
            : base()
        {
        }

        public RspAccountUser(HttpStatusCode codeHttp, AccountUser u = null)
            : base(codeHttp)
        {
            this.result = u == null ? new RspAccountUserResult() :
                new RspAccountUserResult
                {
                    //company = new List<RspAccountUserCompany> { 
                    //    new RspAccountUserCompany 
                    //    { 
                    //        company_name = u.AccountCompany.Name,
                    //        company_id = u.AccountCompany.ID
                    //    } 
                    //},
                    user_name = u.Name,
                    user_username = u.Username,
                    user_email = u.Email
                    //user_language = u.Language
                };
        }

        public RspAccountUserResult result { get; set; }
    }
}