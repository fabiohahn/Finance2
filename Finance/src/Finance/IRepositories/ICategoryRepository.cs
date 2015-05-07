using System.Collections.Generic;

namespace Finance.IRepositories
{
    public interface ICategoryRepository
    {
        IList<Category> GetAll(int propertyId);
        Category Get(int Id);
        Category GetCreditTransferCategory(int propertyId);
        Category GetDebitTransferCategory(int propertyId);
        void Update(Category category, int id);
        void Add(Category category);
        void Remove(Category category);
        void Dispose();
    }
}