using System;
using System.Collections.Generic;
using System.Linq;
using App.Dtos.Report;
using Finance;
using Finance.IRepositories;

namespace App.Reports
{
    public class BalancePerAccount
    {
        private IList<Transaction> _transactions;
        private readonly ITransactionRepository _transactionRepository;

        public BalancePerAccount(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public BallancePerAccountData GetReport(int propertyId, DateTime dateLimit)
        {
            var report = new BallancePerAccountData {DateLimit = dateLimit};

            _transactions = _transactionRepository.GetAll(propertyId, new DateTime(2000, 1, 1), dateLimit);

            var accounts = GetAccountNames();

            foreach (var account in accounts)
            {
                var credits = GetCreditsFromAccount(account);
                var debits = GetDebitsFromAccount(account);

                report.AddData(account, credits - debits);
            }

            return report;
        }

        private IEnumerable<string> GetAccountNames()
        {
            return _transactions.GroupBy(x => x.Account.Name).Select(y => y.First().Account.Name).ToList();
        }

        private decimal GetDebitsFromAccount(string accountName)
        {
            return _transactions.Where(x => x.Account.Name == accountName && (x.TransactionType == TransactionType.Debit || x.TransactionType == TransactionType.DebitTransfer)).Sum(x => x.Value);
        }

        private decimal GetCreditsFromAccount(string accountName)
        {
            return _transactions.Where(x => x.Account.Name == accountName && (x.TransactionType == TransactionType.Credit || x.TransactionType == TransactionType.CreditTransfer)).Sum(x => x.Value);
        }
    }
}