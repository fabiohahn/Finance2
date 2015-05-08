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
    class BallancePerAccountTest
    {
        private Property _property;
        private Mock<ITransactionRepository> _transactionRepository;
        private BalancePerAccount _balancePerAccountReport;

        public BallancePerAccountTest()
        {
            _property = PropertyBuilder.AProperty().Build();
            _transactionRepository = new Mock<ITransactionRepository>();

            _balancePerAccountReport = new BalancePerAccount(_transactionRepository.Object);
        }

        [Fact]
        public void ShouldShowDeBalanceForTwoAccounts()
        {
            var account1 = AccountBuilder.AnAccount().WithProperty(_property).WithName("Conta1").Build();
            var account2 = AccountBuilder.AnAccount().WithProperty(_property).WithName("Conta2").Build();

            var transactions = new List<Transaction>
            {
                TransactionBuilder.ATransaction().WithAccount(account1).WithProperty(_property)
                    .WithType(TransactionType.Debit).WithValue(10m).Build(),
                TransactionBuilder.ATransaction().WithAccount(account1).WithProperty(_property)
                    .WithType(TransactionType.Credit).WithValue(22m).Build(),
                TransactionBuilder.ATransaction().WithAccount(account2).WithProperty(_property)
                    .WithType(TransactionType.Credit).WithValue(17m).Build(),
                TransactionBuilder.ATransaction().WithAccount(account2).WithProperty(_property)
                    .WithType(TransactionType.Debit).WithValue(9m).Build(),
                TransactionBuilder.ATransaction().WithAccount(account2).WithProperty(_property)
                    .WithType(TransactionType.DebitTransfer).WithValue(1m).Build()
            };
            _transactionRepository.Setup(x => x.GetAll(_property.Id, It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(transactions);

            var report = _balancePerAccountReport.GetReport(_property.Id, DateTime.Today);

            Assert.Equal(12m, report.AccountBalance.First(x => x.AccountName == account1.Name).Ballance);
            Assert.Equal(7m, report.AccountBalance.First(x => x.AccountName == account2.Name).Ballance);
        }

        [Fact]
        public void CreditTransferShouldCountOnBalancePerAccount()
        {
            var account = AccountBuilder.AnAccount().WithProperty(_property).Build();

            var transactions = new List<Transaction>
            {
                TransactionBuilder.ATransaction().WithAccount(account).WithProperty(_property)
                    .WithType(TransactionType.Credit).WithValue(15m).Build(),
                TransactionBuilder.ATransaction().WithAccount(account).WithProperty(_property)
                    .WithType(TransactionType.CreditTransfer).WithValue(10m).Build(),
                TransactionBuilder.ATransaction().WithAccount(account).WithProperty(_property)
                    .WithType(TransactionType.Debit).WithValue(7m).Build(),
                TransactionBuilder.ATransaction().WithAccount(account).WithProperty(_property)
                    .WithType(TransactionType.DebitTransfer).WithValue(9m).Build(),
            };
            _transactionRepository.Setup(x => x.GetAll(_property.Id, It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(transactions);

            var report = _balancePerAccountReport.GetReport(_property.Id, DateTime.Today);

            Assert.Equal(9m, report.AccountBalance.First(x => x.AccountName == account.Name).Ballance);
        }
    }
}
