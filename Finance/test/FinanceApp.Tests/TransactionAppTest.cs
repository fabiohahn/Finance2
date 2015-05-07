using System;
using System.Collections.Generic;
using App;
using App.Dtos;
using App.Helpers;
using Finance;
using Finance.IRepositories;
using FinanceTest.Builder;
using Moq;
using XUnit;

namespace FinanceAppTest.Tests
{
    public class TransactionAppTest
    {
        private TransactionApp _transactionApp;
        private Mock<ICategoryRepository> _categoryRepository;
        private Mock<ITransactionRepository> _transactionRepository;
        private Mock<IAccountRepository> _accountRepository;
        private Mock<IPropertyRepository> _propertyRepository;
        private Property _property;
        private Category _category;
        private Account _account;
        private List<Transaction> _transactions;
        private List<Category> _categories;
        private List<Account> _accounts;

        public TransactionAppTest()
        {
            _accountRepository = new Mock<IAccountRepository>();
            _categoryRepository = new Mock<ICategoryRepository>();
            _propertyRepository = new Mock<IPropertyRepository>();
            _transactionRepository = new Mock<ITransactionRepository>();
            _transactionApp = new TransactionApp(_transactionRepository.Object, _accountRepository.Object, _categoryRepository.Object, _propertyRepository.Object);
        
            _property = PropertyBuilder.AProperty().WithId(3).Build();
            _category = CategoryBuilder.ACategory().WithProperty(_property).Build();
            _account = AccountBuilder.AnAccount().WithProperty(_property).Build();
            _categories = new List<Category>() { _category };
            _transactions = new List<Transaction>() { TransactionBuilder.ATransaction().WithProperty(_property).Build() };
            _accounts = new List<Account>() { _account };

            _transactionRepository.Setup(x => x.GetAll(_property.Id, DateTime.Today, 0, 0)).Returns(_transactions);
            _categoryRepository.Setup(x => x.GetAll(_property.Id)).Returns(_categories);
            _accountRepository.Setup(x => x.GetAll(_property.Id)).Returns(_accounts);

            _categoryRepository.Setup(x => x.Get(_category.Id)).Returns(_category);
            _accountRepository.Setup(x => x.Get(_account.Id)).Returns(_account);
            _propertyRepository.Setup(x => x.Get(_property.Id)).Returns((_property));
        }

        [Fact]
        public void GetAllTransactionFromProperty()
        {
            _transactionApp.List(_property.Id, 0, 0, DateTime.Today);

            _transactionRepository.Verify(x => x.GetAll(_property.Id, DateTime.Today, 0, 0), Times.Once);
        }

        [Fact]
        public void ShouldShowAnEmptyTransaction()
        {
            var transaction = _transactionApp.Show(_property.Id, 0);

            Assert.AreEqual(0, transaction.Id);
            Assert.AreEqual(DateTime.Today.ToUsString(), transaction.Date);
        }

        [Fact]
        public void ShouldSaveATransaction()
        {
            var transaction = TransactionBuilder.ATransaction().WithProperty(_property).WithCategory(_category).Build();
            var transactionToSave = new TransactionToSaveDto(transaction);

            _transactionApp.Save(_property.Id, transactionToSave);

            _transactionRepository.Verify(x => x.Add(transaction));
        }

        [Fact]
        public void ShouldDeleteATransaction()
        {
            var transaction = TransactionBuilder.ATransaction().WithProperty(_property).WithCategory(_category).Build();
            _transactionRepository.Setup(x => x.Get(transaction.Id)).Returns(transaction);

            _transactionApp.Remove(_property.Id, transaction.Id);

            _transactionRepository.Verify(x => x.Remove(transaction));
        }

        [Fact]
        public void ShouldGenerateTwoTransactionsOnTrasnfer()
        {
            var creditCategory = CategoryBuilder.ACategory().WithTransactiontype(TransactionType.CreditTransfer).WithProperty(_property).Build();
            var debitCategory = CategoryBuilder.ACategory().WithTransactiontype(TransactionType.DebitTransfer).WithProperty(_property).Build();
            _categoryRepository.Setup(x => x.GetCreditTransferCategory(_property.Id)).Returns(creditCategory);
            _categoryRepository.Setup(x => x.GetDebitTransferCategory(_property.Id)).Returns(debitCategory);

            var originAccount = AccountBuilder.AnAccount().WithId(3).WithProperty(_property).Build();
            var destinyAccount = AccountBuilder.AnAccount().WithId(5).WithProperty(_property).Build();
            var transferTransaction = new TransferToSaveDto(5m, DateTime.Today, originAccount.Id, destinyAccount.Id);

            _transactionApp.Transfer(transferTransaction, _property.Id);

            _transactionRepository.Verify(x => x.Add(It.Is<Transaction>(
                transaction => transaction.Account == originAccount 
                    && transaction.TransactionType == TransactionType.DebitTransfer)));
            _transactionRepository.Verify(x => x.Add(It.Is<Transaction>(
                transaction => transaction.Account == destinyAccount 
                    && transaction.TransactionType == TransactionType.CreditTransfer)));
        }
    }
}
