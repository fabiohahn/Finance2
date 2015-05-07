using System.Collections.Generic;
using Finance.IRepositories;

namespace App
{
    public class CategoryChangerApp
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryChangerApp(ITransactionRepository transactionRepository, ICategoryRepository categoryRepository)
        {
            _transactionRepository = transactionRepository;
            _categoryRepository = categoryRepository;
        }

        public void ChangeCategories(List<int> transactionIds, int categoryId)
        {
            var category = _categoryRepository.Get(categoryId);

            foreach (var transactionId in transactionIds)
            {
                var transaction = _transactionRepository.Get(transactionId);
                transaction.UpdateCategory(category);
                _transactionRepository.Update(transaction, transaction.Id);
            }
        }
    }
}