using System;
using System.Collections.Generic;
using System.Linq;
using App.Reports;
using Finance;
using Finance.IRepositories;
using FinanceTest.Builder;
using Moq;
using Xunit;

namespace FinanceAppTest.Tests.Report
{
    public class TransactionsPerCategoryReportTest
    {
        private Category _categoryA;
        private TransactionPerCategoryReport _balanceReport;
        private Mock<ITransactionRepository> _transactionRepository;
        private readonly DateTime _initialDate = DateTime.Today;
        private readonly DateTime _finalDate = DateTime.Today;
        private const int PropertyId = 1;

        public TransactionsPerCategoryReportTest()
        {
            _categoryA = CategoryBuilder.ACategory().WithTransactiontype(TransactionType.Credit).Build();
            _transactionRepository = new Mock<ITransactionRepository>();
            _balanceReport = new TransactionPerCategoryReport(_transactionRepository.Object);
        }

        [Fact]
        public void ShouldSumTransactionsOfTheSameCategory()
        {           
            var transactions = new List<Transaction>
            {
                TransactionBuilder.ATransaction().WithCategory(_categoryA).WithValue(100).Build(),
                TransactionBuilder.ATransaction().WithCategory(_categoryA).WithValue(60).Build()
            };
            
            _transactionRepository.Setup(x => x.GetAllThatAreNotTransfer(PropertyId, _initialDate, _finalDate)).Returns(transactions);
            
            var report = _balanceReport.GetReport(PropertyId, _initialDate, _finalDate);

            Assert.Equal(160, report.Credits.FirstOrDefault(x => x.Category == _categoryA.Name).Values.FirstOrDefault().Value);
        }

        [Fact]
        public void ShouldSumTransactionsOfTheSameCategoryForMoreThanOneMonth()
        {
            var thisMonth = DateTime.Today;
            var lastMonth = DateTime.Today.AddMonths(-1);

            var transactions = new List<Transaction>
            {
                TransactionBuilder.ATransaction().WithCategory(_categoryA).WithValue(100).Build(),
                TransactionBuilder.ATransaction().WithCategory(_categoryA).WithDate(lastMonth).WithValue(60).Build()
            };
            _transactionRepository.Setup(x => x.GetAllThatAreNotTransfer(PropertyId, lastMonth, thisMonth)).Returns(transactions);

            var report = _balanceReport.GetReport(PropertyId, lastMonth, thisMonth);

            Assert.Equal(100, report.Credits.FirstOrDefault(x => x.Category == _categoryA.Name).Values.FirstOrDefault(x => x.Date.Month == thisMonth.Month).Value);
            Assert.Equal(60, report.Credits.FirstOrDefault(x => x.Category == _categoryA.Name).Values.FirstOrDefault(x => x.Date.Month == lastMonth.Month).Value);
        }

        [Fact]
        public void ShouldSumTransactionsOfMoreThanOneCategory()
        {
            var categoryB = CategoryBuilder.ACategory().WithName("Category B").WithTransactiontype(TransactionType.Credit).Build();

            var transactions = new List<Transaction>
            {
                TransactionBuilder.ATransaction().WithCategory(_categoryA).WithValue(100).Build(),
                TransactionBuilder.ATransaction().WithCategory(categoryB).WithValue(60).Build()
            };
            _transactionRepository.Setup(x => x.GetAllThatAreNotTransfer(PropertyId, _initialDate, _finalDate)).Returns(transactions);

            var report = _balanceReport.GetReport(PropertyId, _initialDate, _finalDate);

            Assert.Equal(100, report.Credits.FirstOrDefault(x => x.Category == _categoryA.Name).Values.FirstOrDefault().Value);
            Assert.Equal(60, report.Credits.FirstOrDefault(x => x.Category == categoryB.Name).Values.FirstOrDefault().Value);
        }
        
        [Fact]
        public void ShouldSegregateCreditsFromDebits()
        {
            var creditCategory = CategoryBuilder.ACategory().WithName("Credit 1").WithTransactiontype(TransactionType.Credit).Build();
            var creditCategory2 = CategoryBuilder.ACategory().WithName("Credit 2").WithTransactiontype(TransactionType.Credit).Build();
            var debitCategory = CategoryBuilder.ACategory().WithName("Debit").WithTransactiontype(TransactionType.Debit).Build();

            var transactions = new List<Transaction>
            {
                TransactionBuilder.ATransaction().WithCategory(creditCategory).WithValue(100).Build(),
                TransactionBuilder.ATransaction().WithCategory(debitCategory).WithValue(60).Build(),
                TransactionBuilder.ATransaction().WithCategory(creditCategory2).WithValue(80).Build()
            };
            _transactionRepository.Setup(x => x.GetAllThatAreNotTransfer(PropertyId, _initialDate, _finalDate)).Returns(transactions);

            var report = _balanceReport.GetReport(PropertyId, _initialDate, _finalDate);

            Assert.Equal(creditCategory.Name, report.Credits.ElementAt(0).Category);
            Assert.Equal(creditCategory2.Name, report.Credits.ElementAt(1).Category);
            Assert.Equal(debitCategory.Name, report.Debits.ElementAt(0).Category);
        }

        [Fact]
        public void ShouldSumCreditsOfTheMonth()
        {
            var categoryB = CategoryBuilder.ACategory().WithName("Category B").WithTransactiontype(TransactionType.Credit).Build();

            var transactions = new List<Transaction>
            {
                TransactionBuilder.ATransaction().WithCategory(_categoryA).WithValue(100).Build(),
                TransactionBuilder.ATransaction().WithCategory(categoryB).WithValue(60).Build()
            };
            _transactionRepository.Setup(x => x.GetAllThatAreNotTransfer(PropertyId, _initialDate, _finalDate)).Returns(transactions);

            var report = _balanceReport.GetReport(PropertyId, _initialDate, _finalDate);

            Assert.Equal(160, report.CreditsSum.Values.FirstOrDefault(x => x.Date == _initialDate).Value);
        }

        [Fact]
        public void ShouldSumCreditsOfMoreThanOneMonth()
        {
            var lastMonth = DateTime.Today.AddMonths(-1);
            var categoryB = CategoryBuilder.ACategory().WithName("Category B").WithTransactiontype(TransactionType.Credit).Build();

            var transactions = new List<Transaction>
            {
                TransactionBuilder.ATransaction().WithCategory(_categoryA).WithValue(100).Build(),
                TransactionBuilder.ATransaction().WithCategory(_categoryA).WithValue(300).WithDate(lastMonth).Build(),
                TransactionBuilder.ATransaction().WithCategory(categoryB).WithValue(60).Build(),
                TransactionBuilder.ATransaction().WithCategory(categoryB).WithValue(40).WithDate(lastMonth).Build()
            };
            _transactionRepository.Setup(x => x.GetAllThatAreNotTransfer(PropertyId, lastMonth, _finalDate)).Returns(transactions);

            var report = _balanceReport.GetReport(PropertyId, lastMonth, _finalDate);
            
            Assert.Equal(340, report.CreditsSum.Values.FirstOrDefault(x => x.Date == lastMonth).Value);
            Assert.Equal(160, report.CreditsSum.Values.FirstOrDefault(x => x.Date == _finalDate).Value);
        }

        [Fact]
        public void ShouldCalculateBalanceOfTheMonth()
        {
            var categoryB = CategoryBuilder.ACategory().WithName("Category B").WithTransactiontype(TransactionType.Debit).Build();

            var transactions = new List<Transaction>
            {
                TransactionBuilder.ATransaction().WithCategory(_categoryA).WithValue(100).Build(),
                TransactionBuilder.ATransaction().WithCategory(categoryB).WithValue(60).Build()
            };
            _transactionRepository.Setup(x => x.GetAllThatAreNotTransfer(PropertyId, _initialDate, _finalDate)).Returns(transactions);

            var report = _balanceReport.GetReport(PropertyId, _initialDate, _finalDate);

            Assert.Equal(40, report.Balance.Values.FirstOrDefault(x => x.Date == _initialDate).Value);
        }
    }
}
