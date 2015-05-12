namespace Sample2015.Test.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NLog;

    public class Sample2015Fixture : IDisposable
    {
        public Sample2015Fixture()
        {
            this.Log = LogManager.GetLogger(this.GetType().FullName);
        }

        public Logger Log { get; private set; }

        public void Dispose()
        {
        }
    }
}
