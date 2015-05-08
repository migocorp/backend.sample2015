namespace Sample2015.Web.Modules
{
    using System;
    using System.Collections.Generic;
    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Responses.Negotiation;
    using Sample2015.Core.BLL;
    using Sample2015.Core.Model.EF;
    using Sample2015.Web.Helper;
    using Sample2015.Web.Helper.Extensions;
    using Sample2015.Web.Helper.Extensions.ModelValidation;
    using Sample2015.Web.Models.Api;
    using Sample2015.Web.Models.Api.Account;

    public class AccountModule : BaseWebModule
    {
        private IAccountService accountService;

        public AccountModule(IAccountService accountService)
            : base()
        {
            this.accountService = accountService;

            this.Get["account-user", "/account/{id:int}"] = this.GetUserById;

            this.Get["account-user-list", "/account/list"] = this.GetUserList;

            this.Post["account-user-create", "/account/create"] = this.CreateAccountUser;

            ////this.Post["account-user-update", "/account/update"] = ;

            ////this.Post["account-user-delete", "/account/delete"] = ;
        }

        private Negotiator GetUserById(dynamic parameters)
        {
            ReqGetUserById req = this.Bind<ReqGetUserById>();

            var user = req.Id > 0 ? this.accountService.GetUserById(req.Id) : null;

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

            IList<string> messages = Sample2015.Web.Helper.ModelValidation.Validate<ReqCreateAccountUser>(this, req);

            if (messages == null)
            {
                messages = ModelValidation.ValidatePassword(req.Password, req.PasswordCheck);
            }

            if (messages != null)
            {
                return Negotiate.WithStatusCode(HttpStatusCode.UnprocessableEntity).WithModel(ModelValidation.WrongValidationModel(req, messages, this)).WithView("Add.sshtml");
            }

            var accountUser = new AccountUser()
            {
                Email = req.Email,
                Name = req.Name,
                Password = req.Password,
                Username = req.Username
            };

            this.accountService.Add(accountUser);

            return Negotiate.WithOnlyJson(new RspFrame());
        }
    }
}