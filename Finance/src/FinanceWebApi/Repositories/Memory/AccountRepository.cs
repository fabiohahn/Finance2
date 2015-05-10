using System.Collections.Generic;
using System.Linq;
using Finance;
using Finance.IRepositories;

namespace FinanceMvc.Repositories.Memory
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public IList<Account> GetAll(int propertyId)
        {
            var data = Data.Values.OfType<Account>().ToList();
            return data.Where(x => x.Property.Id == propertyId).ToList();
        }
    }
}