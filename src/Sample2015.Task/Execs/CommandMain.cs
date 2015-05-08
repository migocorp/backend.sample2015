namespace Sample2015.Task.Execs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Sample2015.Core.BLL;
    using TinyIoC;

    public class CommandMain
    {
        public static void Main()
        {
            Console.WriteLine("Command program Start");

            try
            {
                CommandMain main = new CommandMain();
                var acctService = main.GetAccountService();
                Console.WriteLine(acctService.GetUserById(1));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            Console.WriteLine("Command program End");
        }

        public IAccountService GetAccountService()
        {
            var ctx = TinyIoCContainer.Current;
            ctx.AutoRegister();
            var acctService = ctx.Resolve<IAccountService>();
            return acctService;
        }
    }
}
