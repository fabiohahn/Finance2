using System.Collections.Generic;

namespace Finance.IRepositories
{
    public interface IAccountRepository
    {
        IList<Account> GetAll(int propertyId);
        Account Get(int id);
        void Update(Account account, int id);
        void Add(Account account);
        void Remove(Account account);
        void Dispose();
    }
}