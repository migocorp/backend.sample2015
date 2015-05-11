namespace Sample2015.Core.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Sample2015.Core.Model.EF;

    public interface IAccountService
    {
        AccountUser GetUserById(int id);

        IEnumerable<AccountUser> FindAll();

        void Add(AccountUser accountUser);

        void Update(AccountUser accountUser);

        void Delete(int id);
    }
}
