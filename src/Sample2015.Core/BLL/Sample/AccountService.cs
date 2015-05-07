namespace Sample2015.Core.BLL.Sample
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Sample2015.Core.DAL.DbContextScope;
    using Sample2015.Core.DAL.Repo.CategoryA;
    using Sample2015.Core.Model.EF;

    public class AccountService : IAccountService
    {
        private readonly IRepoAccountUser repoAccountUser;

        public AccountService(IRepoAccountUser repoAccountUser)
        {
            this.repoAccountUser = repoAccountUser;
        }

        public AccountUser GetUserById(int id)
        {
            using (var scope = new DbContextScope(DbContextScopePurpose.Reading))
            {
                return this.repoAccountUser.GetByID(id);
            }
        }
    }
}
