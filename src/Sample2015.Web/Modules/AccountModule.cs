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
        public static readonly string PathApiAccountUser = "/account/{id:int}";
        public static readonly string PathApiAccountUserList = "/account/list";
        public static readonly string PathApiAccountUserCreate = "/account/create";

        public AccountModule(IAccountService accountService)
            : base()
        {
            this.AccountService = accountService;

            this.Get["account-user", PathApiAccountUser] = this.GetUserById;

            this.Get["account-user-list", PathApiAccountUserList] = this.GetUserList;

            this.Post["account-user-create", PathApiAccountUserCreate] = _ => this.RunHandler<ReqCreateAccountUser, Negotiator>(this.CreateAccountUser);

            this.Put["account-user-update", PathApiAccountUser] = _ => this.RunHandler<ReqUpdateAccountUser, Negotiator>(this.UpdateAccountUser);

            this.Delete["account-user-delete", PathApiAccountUser] = this.DeleteAccountUser;
        }

        public IAccountService AccountService { get; set; }

        private Negotiator GetUserById(dynamic parameters)
        {
            ReqGetUserById req = this.Bind<ReqGetUserById>();

            var user = req.Id > 0 ? this.AccountService.GetUserById(req.Id) : null;

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
            var users = this.AccountService.FindAll();

            return Negotiate.WithOnlyJson(
                new RspAccountUserList(HttpStatusCode.OK, users), HttpStatusCode.OK);
        }

        private Negotiator CreateAccountUser(dynamic parameters)
        {
            ReqCreateAccountUser req = this.Bind<ReqCreateAccountUser>();

            var user = this.AccountService.GetUserByUsername(req.Username);

            if (user != null)
            {
                return Negotiate.WithOnlyJson(new RspFrame(HttpStatusCode.InternalServerError));
            }

            IList<string> messages = Sample2015.Web.Helper.ModelValidation.Validate<ReqCreateAccountUser>(this, req);

            if (messages == null)
            {
                messages = ModelValidation.ValidatePassword(req.Password, req.PasswordCheck);
            }

            if (messages != null)
            {
                return Negotiate.WithStatusCode(HttpStatusCode.UnprocessableEntity).WithModel(ModelValidation.WrongValidationModel(req, messages, this));
            }

            var accountUser = new AccountUser()
            {
                Email = req.Email,
                Name = req.Name,
                Password = req.Password,
                Username = req.Username
            };

            this.AccountService.Add(accountUser);

            return Negotiate.WithOnlyJson(new RspFrame(HttpStatusCode.OK));
        }

        private Negotiator UpdateAccountUser(dynamic parameters)
        {
            ReqUpdateAccountUser req = this.Bind<ReqUpdateAccountUser>();

            var user = this.AccountService.GetUserById(req.Id);

            if (user == null)
            {
                return Negotiate.WithOnlyJson(new RspFrame() { code = Convert.ToInt32(HttpStatusCode.NotFound) });
            }

            IList<string> messages = Sample2015.Web.Helper.ModelValidation.Validate<ReqUpdateAccountUser>(this, req);

            if (messages != null)
            {
                return Negotiate.WithStatusCode(HttpStatusCode.UnprocessableEntity).WithModel(ModelValidation.WrongValidationModel(req, messages, this));
            }

            user.Email = string.IsNullOrEmpty(req.Email) ? user.Email : req.Email;
            user.Name = string.IsNullOrEmpty(req.Name) ? user.Email : req.Name;
            user.DateModified = DateTime.Now;

            this.AccountService.Update(user);

            return Negotiate.WithOnlyJson(new RspFrame(HttpStatusCode.OK));
        }

        private Negotiator DeleteAccountUser(dynamic parameters)
        {
            ReqDeleteAccountUser req = this.Bind<ReqDeleteAccountUser>();

            var user = this.AccountService.GetUserById(req.Id);

            if (user == null)
            {
                return Negotiate.WithOnlyJson(new RspFrame() { code = Convert.ToInt32(HttpStatusCode.NotFound) });
            }

            this.AccountService.Delete(req.Id);

            return Negotiate.WithOnlyJson(new RspFrame(HttpStatusCode.OK));
        }
    }
}