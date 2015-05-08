namespace Sample2015.Core.BLL
{
    using System;
    using NLog;

    public abstract class BaseService
    {
        protected readonly Logger Log;

        public BaseService()
        {
            this.Log = LogManager.GetLogger(this.GetType().Name);
        }
    }
}
