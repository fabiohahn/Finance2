using System;
using System.Collections.Generic;
using Finance;

namespace App.Dtos
{
    public class TransactionListDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int AccountId { get; set; }
        public int CategoryId { get; set; }
        public IList<TransactionListItemDto> Transactions { get; set; }
        public IList<AccountDto> Accounts { get; set; }
        public IList<CategoryDto> Categories { get; set; }

        public TransactionListDto(IEnumerable<Transaction> transactions, IEnumerable<Account> accounts, IEnumerable<Category> categories)
        {
            Month = DateTime.Today.Month;
            Year = DateTime.Today.Year;
            TransformTransactions(transactions);
            TransformCategories(categories);
            TransformAccounts(accounts);
        }

        public TransactionListDto(IList<Transaction> transactions, IList<Account> accounts, IList<Category> categories, int accountId, int categoryId, DateTime referenceDate)
        {
            AccountId = accountId;
            CategoryId = categoryId;
            Month = referenceDate.Month;
            Year = referenceDate.Year;
            TransformTransactions(transactions);
            TransformCategories(categories);
            TransformAccounts(accounts);
        }

        private void TransformCategories(IEnumerable<Category> categories)
        {
            Categories = new List<CategoryDto>();

            foreach (var category in categories)
            {
                Categories.Add(new CategoryDto(category));
            }
        }

        private void TransformAccounts(IEnumerable<Account> accounts)
        {
            Accounts = new List<AccountDto>();

            foreach (var account in accounts)
            {
                Accounts.Add(new AccountDto(account));
            }
        }

        private void TransformTransactions(IEnumerable<Transaction> transactions)
        {
            Transactions = new List<TransactionListItemDto>();

            foreach (var transaction in transactions)
            {
                Transactions.Add(new TransactionListItemDto(transaction));
            }
        }
    }
}