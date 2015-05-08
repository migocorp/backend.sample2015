namespace Sample2015.Web.Modules
{
    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Responses.Negotiation;
    using Sample2015.Core.BLL;
    using Sample2015.Core.Model.EF;
    using Sample2015.Web.Helper;
    using Sample2015.Web.Helper.Extensions;
    using Sample2015.Web.Models.Api;
    using Sample2015.Web.Models.Api.Account;

    public class AccountModule : BaseWebModule
    {
        private IAccountService accountService;

        public AccountModule(IAccountService accountService)
            : base()
        {
            this.accountService = accountService;

            this.Get["account-user", "/account/user/{id:int}"] = this.GetUserById;

            this.Get["account-user-list", "account/userlist"] = this.GetUserList;

            this.Post["account-user-create", "account"] = this.CreateAccountUser;
        }

        private Negotiator GetUserById(dynamic parameters)
        {
            ReqGetUserById req = this.Bind<ReqGetUserById>();

            var user = req.id > 0 ? this.accountService.GetUserById(req.id) : null;

            if (user == null)
            {
                return Negotiate.WithOnlyJson(
                    new RspAccountUser(HttpStatusCode.NotFound), HttpStatusCode.NotFound);
            }

            return Negotiate.WithOnlyJson(
                new RspAccountUser(HttpStatusCode.OK, user), HttpStatusCode.OK);
        }

        private Negotiator GetUserList(dynamic parameters)
        {
            ReqGetUserById req = this.Bind<ReqGetUserById>();

            var users = this.accountService.FindAll();

            return Negotiate.WithOnlyJson(
                new RspAccountUserList(HttpStatusCode.OK, users), HttpStatusCode.OK);
        }

        private Negotiator CreateAccountUser(dynamic parameters)
        {
            ReqCreateAccountUser req = this.Bind<ReqCreateAccountUser>();

            var accountUser = new AccountUser()
            {
                Email = req.email,
                Name = req.name,
                Password = req.password,
                Username = req.username
            };

            this.accountService.Add(accountUser);

            return Negotiate.WithOnlyJson(new RspFrame());
        }
    }
}