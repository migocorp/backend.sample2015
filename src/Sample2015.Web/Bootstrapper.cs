namespace Sample2015.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Nancy;
    using Nancy.Conventions;

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            nancyConventions.StaticContentsConventions.AddDirectory("docs", "swagger-ui");
        }
    }
}