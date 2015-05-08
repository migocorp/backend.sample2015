namespace Sample2015.Web.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Sample2015.Web.Models.Api;
    using Sample2015.Web.Models.Api.Account;
    using Nancy;
    using Nancy.Metadata.Modules;
    using Nancy.Swagger;
    using Swagger.ObjectModel.ApiDeclaration;

    public class AccountMetadataModule : MetadataModule<SwaggerRouteData>
    {
        public AccountMetadataModule()
        {
            this.Describe["account-user"] = description => description.AsSwagger(with =>
            {
                with.ResourcePath("/api/account");
                with.Summary("(*Ok)取得用戶訊息");
                with.Notes("取得用戶訊息");
                with.PathParam<int>("id", "User's ID", true, 0);
                with.Model<RspAccountUser>();
            });
        }
    }
}