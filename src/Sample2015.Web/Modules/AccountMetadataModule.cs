namespace Sample2015.Web.Modules
{
    using Nancy.Metadata.Modules;
    using Nancy.Swagger;
    using Sample2015.Web.Models.Api;
    using Sample2015.Web.Models.Api.Account;

    public class AccountMetadataModule : MetadataModule<SwaggerRouteData>
    {
        public AccountMetadataModule()
        {
            this.Describe["account-user"] = description => description.AsSwagger(with =>
            {
                with.ResourcePath("/api/account");
                with.Summary("取得單一使用者資料");
                with.Notes("取得單一使用者資料");
                with.PathParam<int>("id", "User's ID", true, 1);
                with.Model<RspAccountUser>();
            });

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
                with.Summary("使用使用者");
                with.Notes("使用使用者");
                with.PathParam<int>("id", "User's Name", true, 0);
                with.PathParam<int>("id", "User's Username", true, 0);
                with.PathParam<int>("id", "User's Password", true, 0);
                with.PathParam<int>("id", "User's Email", true, 0);
                with.Model<RspFrame>();
            });
        }
    }
}