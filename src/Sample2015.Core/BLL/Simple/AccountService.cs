namespace Sample2015.Core.BLL.Simple
{
    using System.Collections.Generic;
    using System.Linq;
    using Sample2015.Core.DAL.DbContextScope;
    using Sample2015.Core.DAL.Repo.CategoryA;
    using Sample2015.Core.Model.EF;

    public class AccountService : BaseService, IAccountService
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

        public IEnumerable<AccountUser> FindAll()
        {
            using (var scope = new DbContextScope(DbContextScopePurpose.Reading))
            {
                return this.repoAccountUser.Find();
            }
        }

        public void Add(AccountUser accountUser)
        {
            using (var scope = new DbContextScope(DbContextScopePurpose.Writing))
            {
                this.repoAccountUser.Insert(accountUser);

                scope.SaveChanges();
            }
        }

        public void Update(AccountUser accountUser)
        {
            using (var scope = new DbContextScope(DbContextScopePurpose.Writing))
            {
                this.repoAccountUser.Update(accountUser);

                scope.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var scope = new DbContextScope(DbContextScopePurpose.Writing))
            {
                this.repoAccountUser.DeleteByID(id);

                scope.SaveChanges();
            }
        }

        public AccountUser GetUserByUsername(string username)
        {
            using (var scope = new DbContextScope(DbContextScopePurpose.Reading))
            {
                return this.repoAccountUser.Find(u => u.Username.Equals(username)).SingleOrDefault();
            }
        }
    }
}
