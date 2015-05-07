namespace Sample2015.Core.DAL.Repo.CategoryA.EF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Sample2015.Core.DAL.DbContextScope;
    using Sample2015.Core.Model.EF;

    public class RepoAccountUser : GenericRepository<AccountUser>, IRepoAccountUser
    {
        public RepoAccountUser(IAmbientDbContextLocator ambientDbContextLocator) : base(ambientDbContextLocator)
        {
        }
    }
}
