using System;
using System.Collections.Generic;
using System.Linq;
using App.Helpers;
using Finance;

namespace App.Dtos
{
    public class TransactionToSaveDto
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public string Date { get; set; }
        public int CategoryId { get; set; }
        public int AccountId { get; set; }
        public string Description { get; set; }
        public ICollection<CategoryDto> Categories { get; set; }
        public ICollection<AccountDto> Accounts { get; set; }

        public TransactionToSaveDto()
        {
            Id = 0;
            Date = DateTime.Today.ToUsString();
        }

        public TransactionToSaveDto(IEnumerable<Category> categories, IEnumerable<Account> accounts, Transaction transaction)
        {
            Id = 0;
            Date = DateTime.Today.ToUsString();
            TransformCategories(categories);
            TransformAccounts(accounts);
            
            if(Categories.Count > 0)
                CategoryId = Categories.FirstOrDefault().Id;
            
            if(Accounts.Count > 0)
                AccountId = Accounts.FirstOrDefault().Id;

            if (transaction != null && transaction.Id != 0)
                UpdateTransactionData(transaction);
        }

        public TransactionToSaveDto(Transaction transaction)
        {
            UpdateTransactionData(transaction);
        }

        private void UpdateTransactionData(Transaction transaction)
        {
            Id = transaction.Id;
            Value = transaction.Value;
            Date = transaction.Date.ToUsString();
            Description = transaction.Description;
            CategoryId = transaction.Category.Id;
            AccountId = transaction.Account.Id;
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
    }
}