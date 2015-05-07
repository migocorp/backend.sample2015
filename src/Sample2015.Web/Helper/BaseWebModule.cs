namespace Sample2015.Web.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Nancy;
    using NLog;

    public class BaseWebModule : NancyModule
    {
        public static readonly string PathApiBase = "/api";
        protected readonly Logger Log;

        public BaseWebModule()
            : base(BaseWebModule.PathApiBase)
        {
            this.Log = LogManager.GetLogger(this.GetType().Name);
        }

        public BaseWebModule(string modulePath)
            : base(modulePath)
        {
            this.Log = LogManager.GetLogger(this.GetType().Name);
        }
    }
}