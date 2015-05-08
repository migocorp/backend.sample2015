namespace Sample2015.Task.Job
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Quartz;
    using Sample2015.Core.BLL;
    using TinyIoC;

    [DisallowConcurrentExecution]
    public class FetchAccountUser : BaseJob, IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            this.Log.Info("Exec job Start: FetchAccountUser.");

            try
            {
                var acctService = this.GetAccountService();
                this.Log.Info(acctService.GetUserById(1));
            }
            catch (Exception ex)
            {
                this.Log.Error(ex.Message);
                this.Log.Error(ex.StackTrace);
            }

            this.Log.Info("Exec job End: FetchAccountUser.");
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
