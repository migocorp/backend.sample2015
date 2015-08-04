[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed")]

namespace Sample2015.Web.ModelDataProviders.Api.Account
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Nancy.Swagger;
    using Nancy.Swagger.Services;
    using Sample2015.Web.Models.Api.Account;

    public class RspAccountUserModelDataProvider : ISwaggerModelDataProvider
    {
        public SwaggerModelData GetModelData()
        {
            return SwaggerModelData.ForType<RspAccountUser>(with =>
            {
                with.Description("user data");

                with.Property(x => x.code)
                    .Required(true);

                with.Property<RspAccountUserResultData>(x => x.result)
                    .Required(true);
            });
        }
    }

    public class RspAccountUserResultDataModelDataProvider : ISwaggerModelDataProvider
    {
        public SwaggerModelData GetModelData()
        {
            return SwaggerModelData.ForType<RspAccountUserResultData>(with =>
            {
                with.Description("user data detail");

                with.Property(x => x.user_username)
                    .Required(true)
                    .Description("使用者帳號");

                with.Property(x => x.user_email)
                    .Required(true)
                    .Description("使用者email");

                with.Property(x => x.user_name)
                    .Required(true)
                    .Description("使用者名稱");
            });
        }
    }
}