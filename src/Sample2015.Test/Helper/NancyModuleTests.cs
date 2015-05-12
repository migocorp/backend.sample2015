namespace Sample2015.Test.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NLog;
    using Xunit;
    using Xunit.Abstractions;

    public class NancyModuleTests : IClassFixture<Sample2015Fixture>
    {
        public NancyModuleTests(Sample2015Fixture fixture)
        {
            this.Log = LogManager.GetLogger(this.GetType().FullName);
        }

        public NancyModuleTests(Sample2015Fixture fixture, ITestOutputHelper output) : this(fixture)
        {
            this.Console = output;
        }

        protected Logger Log { get; private set; }

        protected ITestOutputHelper Console { get; set; }

        public void Output(string msg)
        {
            this.Console.WriteLine(msg);
        }

        public void Output(string msg, params object[] args)
        {
            this.Console.WriteLine(string.Format(msg, args));
        }
    }
}
