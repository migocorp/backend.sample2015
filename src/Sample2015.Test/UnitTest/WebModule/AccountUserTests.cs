namespace Sample2015.Test.UnitTest.WebModule
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Nancy;
    using Nancy.Bootstrapper;
    using Nancy.Testing;
    using Sample2015.Core.BLL.Simple;
    using Sample2015.Core.DAL.DbContextScope;
    using Sample2015.Core.DAL.Repo.CategoryA.EF;
    using Sample2015.Core.Helper;
    using Sample2015.Test.Helper;
    using Sample2015.Web.Helper;
    using Sample2015.Web.Models.Api;
    using Sample2015.Web.Models.Api.Account;
    using Sample2015.Web.Modules;
    using Xunit;
    using Xunit.Abstractions;

    public class AccountUserTests : NancyModuleTests
    {
        private readonly INancyBootstrapper bootstrapper;
        private readonly Browser browser;

        public AccountUserTests(Sample2015Fixture fixture, ITestOutputHelper output)
            : base(fixture, output)
        {
            this.bootstrapper = new ConfigurableBootstrapper(
                    configuration =>
                    {
                        configuration.Dependency<AmbientDbContextLocator>();
                        configuration.Dependency<RepoAccountUser>();
                        configuration.Dependency<AccountService>();
                        configuration.Module<AccountModule>();
                    });

            this.browser = new Browser(this.bootstrapper, defaults: to => to.Accept("application/json"));
        }

        [Fact]
        public void GetAccountUser()
        {
            var key = @"{id:int}";
            var url = BaseWebModule.PathApiBase + AccountModule.PathApiAccountUser;
            var rsp = this.browser.Get(url.Replace(key, "1"));

            var rspStr = rsp.Body.AsString();
            var rspModel = KitStr.DeserializeObject<RspAccountUser>(rspStr);
            this.Output(string.Format("Http Response Code: {0}, Json:{1}", rsp.StatusCode, KitStr.SerializeObject(rspModel, true)));
            Assert.Equal((int)HttpStatusCode.OK, rspModel.code);
        }

        [Fact]
        public void GetAccountUserList()
        {
            var url = BaseWebModule.PathApiBase + AccountModule.PathApiAccountUserList;
            var rsp = this.browser.Get(url);

            var rspStr = rsp.Body.AsString();
            var rspModel = KitStr.DeserializeObject<RspAccountUserList>(rspStr);
            this.Output(string.Format("Http Response Code: {0}, Json:{1}", rsp.StatusCode, KitStr.SerializeObject(rspModel, true)));
            Assert.Equal((int)HttpStatusCode.OK, rspModel.code);
        }

        [Fact]
        public void CreateAccountUser()
        {
            var url = BaseWebModule.PathApiBase + AccountModule.PathApiAccountUserCreate;
            var rsp = this.browser.Post(
                url,
                with =>
                {
                    with.FormValue("username", "asus");
                    with.FormValue("password", "asus1234");
                    with.FormValue("passwordcheck", "asus1234");
                    with.FormValue("email", "admin@asus.com");
                    with.FormValue("name", "華碩");
                });

            var rspStr = rsp.Body.AsString();
            var rspModel = KitStr.DeserializeObject<RspFrame>(rspStr);
            this.Output(string.Format("Http Response Code: {0}, Json:{1}", rsp.StatusCode, KitStr.SerializeObject(rspModel, true)));
            Assert.Equal((int)HttpStatusCode.OK, rspModel.code);
        }

        [Fact]
        public void UpdateAccountUser()
        {
            var config = (ConfigurableBootstrapper)this.bootstrapper;
            var module = (AccountModule)config.GetModule(typeof(AccountModule), new NancyContext());
            var user = module.AccountService.GetUserByUsername("asus");
            if (user != null)
            {
                var key = @"{id:int}";
                var url = BaseWebModule.PathApiBase + AccountModule.PathApiAccountUser;
                var rsp = this.browser.Put(
                    url.Replace(key, user.ID.ToString()),
                    with =>
                    {
                        with.FormValue("name", "ASUS");
                    });

                var rspStr = rsp.Body.AsString();
                var rspModel = KitStr.DeserializeObject<RspFrame>(rspStr);
                this.Output(string.Format("Http Response Code: {0}, Json:{1}", rsp.StatusCode, KitStr.SerializeObject(rspModel, true)));
                Assert.Equal((int)HttpStatusCode.OK, rspModel.code);
            }
            else
            {
                Assert.NotNull(user);
            }
        }

        [Fact]
        public void DeleteAccountUser()
        {
            var config = (ConfigurableBootstrapper)this.bootstrapper;
            var module = (AccountModule)config.GetModule(typeof(AccountModule), new NancyContext());
            var user = module.AccountService.GetUserByUsername("asus");
            if (user != null)
            {
                var key = @"{id:int}";
                var url = BaseWebModule.PathApiBase + AccountModule.PathApiAccountUser;
                var rsp = this.browser.Delete(url.Replace(key, user.ID.ToString()));

                var rspStr = rsp.Body.AsString();
                var rspModel = KitStr.DeserializeObject<RspFrame>(rspStr);
                this.Output(string.Format("Http Response Code: {0}, Json:{1}", rsp.StatusCode, KitStr.SerializeObject(rspModel, true)));
                Assert.Equal((int)HttpStatusCode.OK, rspModel.code);
            }
            else
            {
                Assert.NotNull(user);
            }
        }
    }
}
