using System.Collections.Generic;
using System.Linq;
using Finance;
using Finance.IRepositories;

namespace FinanceMvc.Repositories.Memory
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public IList<Category> GetAll(int propertyId)
        {
            var data = Data.Values.OfType<Category>().ToList();
            return data.Where(x => x.Property.Id == propertyId).ToList();
        }

        public Category GetCreditTransferCategory(int propertyId)
        {
            var data = Data.Values.OfType<Category>().ToList();
            return data.FirstOrDefault(x => x.Property.Id == propertyId && x.TransactionType == TransactionType.CreditTransfer);
        }

        public Category GetDebitTransferCategory(int propertyId)
        {
            var data = Data.Values.OfType<Category>().ToList();
            return data.FirstOrDefault(x => x.Property.Id == propertyId && x.TransactionType == TransactionType.CreditTransfer);
        }
    }
}