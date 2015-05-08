namespace Sample2015.Task.Job
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NLog;

    public class BaseJob
    {
        protected readonly Logger Log;

        public BaseJob()
        {
            this.Log = LogManager.GetLogger(this.GetType().Name);
        }
    }
}
