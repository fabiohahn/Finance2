using System.Collections.Generic;
using System.Linq;
using App.Dtos;
using Finance;
using Finance.IRepositories;

namespace App
{
    public class CategoryApp
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly ITransactionRepository _transactionRepository;

        public CategoryApp(ICategoryRepository categoryRepository, IPropertyRepository propertyRepository, ITransactionRepository transactionRepository)
        {
            _categoryRepository = categoryRepository;
            _propertyRepository = propertyRepository;
            _transactionRepository = transactionRepository;
        }

        public IList<CategoryDto> List(int propertyId)
        {
            var categorys = _categoryRepository.GetAll(propertyId);

            return categorys.Select(category => new CategoryDto(category)).ToList();
        }

        public CategoryDto Show(int propertyId, int categoryId)
        {
            var category = _categoryRepository.Get(categoryId);

            return new CategoryDto(category);
        }

        public void Save(int propertyId, CategoryDto categoryToSave)
        {
            var property = _propertyRepository.Get(propertyId);

            var category = new Category(categoryToSave.Name, property, categoryToSave.TransactionType);

            if (categoryToSave.Id != 0)
                _categoryRepository.Update(category, categoryToSave.Id);
            else
                _categoryRepository.Add(category);
        }

        public void Remove(int propertyId, int id)
        {
            var category = _categoryRepository.Get(id);
            var haveTransaction = _transactionRepository.HasTransactionWithCategory(category.Id);

            if(haveTransaction)
                throw  new DomainException("Categoria está registrada em uma transação");

            if (category.Property.Id != propertyId)
                throw new DomainException("Categoria não pode ser removida");

            _categoryRepository.Remove(category);
        }
    }
}
