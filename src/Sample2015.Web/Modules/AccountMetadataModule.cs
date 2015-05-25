namespace Sample2015.Web.Modules
{
    using Nancy.Metadata.Modules;
    using Nancy.Routing;
    using Nancy.Swagger;
    using Sample2015.Web.Models.Api;
    using Sample2015.Web.Models.Api.Account;
    using Swagger.ObjectModel.ApiDeclaration;

    public class AccountMetadataModule : MetadataModule<SwaggerRouteData>
    {
        public AccountMetadataModule()
        {
            this.Describe["account-user"] = this.AccountUser;

            this.Describe["account-user-list"] = description => description.AsSwagger(with =>
            {
                with.ResourcePath("/api/account");
                with.Summary("取得使用者列表資料");
                with.Notes("取得使用者列表資料");
                with.Model<RspAccountUserList>();
            });

            this.Describe["account-user-create"] = description => description.AsSwagger(with =>
            {
                with.ResourcePath("/api/account");
                with.Summary("新增使用者");
                with.Notes("新增使用者");
                with.Param<string>(ParameterType.Form, "Username", "User's Username", true);
                with.Param<string>(ParameterType.Form, "Password", "User's Password", true);
                with.Param<string>(ParameterType.Form, "PasswordCheck", "User's Password Check", true);
                with.Param<string>(ParameterType.Form, "Email", "User's Email", true);
                with.Param<string>(ParameterType.Form, "Name", "User's Name", true);
                with.Model<RspFrame>();
            });

            this.Describe["account-user-update"] = description => description.AsSwagger(with =>
            {
                with.ResourcePath("/api/account");
                with.Summary("修改使用者");
                with.Notes("修改使用者");
                with.Param<int>(ParameterType.Path, "Id", "User's Id", true);
                with.Param<string>(ParameterType.Form, "Email", "User's Email");
                with.Param<string>(ParameterType.Form, "Name", "User's Name");
                with.Model<RspFrame>();
            });

            this.Describe["account-user-delete"] = description => description.AsSwagger(with =>
            {
                with.ResourcePath("/api/account");
                with.Summary("刪除使用者");
                with.Notes("刪除使用者");
                with.Param<int>(ParameterType.Path, "Id", "User's Id", true);
                with.Model<RspFrame>();
            });
        }

        public SwaggerRouteData AccountUser(RouteDescription description)
        {
            return description.AsSwagger(with =>
            {
                with.ResourcePath("/api/account");
                with.Summary("取得單一使用者資料");
                with.Notes("取得單一使用者資料");
                with.PathParam<int>("id", "User's ID", true, 1);
                with.Model<RspAccountUser>();
            });
        }
    }
}