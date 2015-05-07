using System.Collections.Generic;
using App;
using Finance;
using Finance.IRepositories;
using FinanceTest.Builder;
using Moq;
using Xunit;

namespace FinanceAppTest.Tests
{
    public class CategoryChangerAppTest
    {
        private Category _category;
        private Property _property;
        private Account _account;
        private Mock<ITransactionRepository> _transactionRepository;
        private Mock<ICategoryRepository> _categoryRepository;
        private Mock<IAccountRepository> _accountRepository;
        private Mock<IPropertyRepository> _propertyRepository;
        private CategoryChangerApp _categoryChangerApp;

        public CategoryChangerAppTest()
        {
            _accountRepository = new Mock<IAccountRepository>();
            _categoryRepository = new Mock<ICategoryRepository>();
            _propertyRepository = new Mock<IPropertyRepository>();
            _transactionRepository = new Mock<ITransactionRepository>();
        
            _property = PropertyBuilder.AProperty().WithId(3).Build();
            _category = CategoryBuilder.ACategory().WithProperty(_property).WithId(3).Build();
            _account = AccountBuilder.AnAccount().WithProperty(_property).Build();

            _categoryRepository.Setup(x => x.Get(_category.Id)).Returns(_category);
            _accountRepository.Setup(x => x.Get(_account.Id)).Returns(_account);
            _propertyRepository.Setup(x => x.Get(_property.Id)).Returns((_property));

            _categoryChangerApp = new CategoryChangerApp(_transactionRepository.Object, _categoryRepository.Object);
        }

        [Fact]
        public void ShouldChangeTheCategoryOfTwoTransactions()
        {
            var firstTransaction = TransactionBuilder.ATransaction().WithProperty(_property).WithId(2).Build();
            var secondTransaction = TransactionBuilder.ATransaction().WithProperty(_property).WithId(3).Build();
            var transactionsIds = new List<int>() { firstTransaction.Id, secondTransaction.Id };
            _transactionRepository.Setup(x => x.Get(firstTransaction.Id)).Returns(firstTransaction);
            _transactionRepository.Setup(x => x.Get(secondTransaction.Id)).Returns(secondTransaction);

            _categoryChangerApp.ChangeCategories(transactionsIds, _category.Id);

            _transactionRepository.Verify(x => x.Update(It.Is<Transaction>(transaction => transaction.Category == _category), firstTransaction.Id));
            _transactionRepository.Verify(x => x.Update(It.Is<Transaction>(transaction => transaction.Category == _category), secondTransaction.Id));
        }        
    }
}
