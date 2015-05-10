using System;
using System.Collections.Generic;
using System.Linq;
using Finance;
using Finance.IRepositories;

namespace FinanceMvc.Repositories.Memory
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        public IList<Transaction> GetAllThatAreNotTransfer(int propertyId, DateTime initialDate, DateTime finalDate)
        {
            var data = Data.Values.OfType<Transaction>().ToList();
            return data.Where(x => x.Property.Id == propertyId 
                                   && initialDate <= x.Date 
                                   && x.Date <= finalDate 
                                   && x.TransactionType != TransactionType.CreditTransfer 
                                   && x.TransactionType != TransactionType.DebitTransfer)
                .ToList();
        }

        public IList<Transaction> GetAll(int propertyId, DateTime date, int accountId, int categoryId)
        {
            if (accountId != 0 && categoryId != 0)
                return GetAllByAccountAndCategory(propertyId, date, accountId, categoryId);
            if (accountId != 0 && categoryId == 0)
                return GetAllByAccount(propertyId, date, accountId);
            if (accountId == 0 && categoryId != 0)
                return GetAllByCategory(propertyId, date, categoryId);

            return GetAllByDate(propertyId, date);
        }

        public IList<Transaction> GetAllByAccountAndCategory(int propertyId, DateTime date, int accountId, int categoryId)
        {
            var data = Data.Values.OfType<Transaction>().ToList();
            return data.Where(t => t.Property.Id == propertyId && t.Date.Month == date.Month && t.Date.Year == date.Year && t.Account.Id == accountId && t.Category.Id == categoryId)
                     .OrderBy(x => x.Date)
                    .ToList();
        }

        public IList<Transaction> GetAllByAccount(int propertyId, DateTime date, int accountId)
        {
            var data = Data.Values.OfType<Transaction>().ToList();
            return data.Where(t => t.Property.Id == propertyId && t.Date.Month == date.Month && t.Date.Year == date.Year && t.Account.Id == accountId)
                    .OrderBy(x => x.Date)
                    .ToList();
        }

        public IList<Transaction> GetAllByCategory(int propertyId, DateTime date, int categoryId)
        {
            var data = Data.Values.OfType<Transaction>().ToList();
            return data.Where(t => t.Property.Id == propertyId && t.Date.Month == date.Month && t.Date.Year == date.Year && t.Category.Id == categoryId)
                    .OrderByDescending(x => x.Value)
                    .ToList();
        }

        public IList<Transaction> GetAllByDate(int propertyId, DateTime date)
        {
            var data = Data.Values.OfType<Transaction>().ToList();
            return data.Where(t => t.Property.Id == propertyId && t.Date.Month == date.Month && t.Date.Year == date.Year)
                    .OrderByDescending(x => x.Date)
                    .ToList();
        }

        public IList<Transaction> GetAll(int propertyId, DateTime initialDate, DateTime finalDate)
        {
            var data = Data.Values.OfType<Transaction>().ToList();
            return data.Where(t => t.Property.Id == propertyId && initialDate <= t.Date && t.Date <= finalDate)
                .ToList();
        }

        public IList<Transaction> GetAll(int propertyId)
        {
            var data = Data.Values.OfType<Transaction>().ToList();
            return data.Where(t => t.Property.Id == propertyId)
                .OrderBy(x => x.Date)
                .ToList();
        }

        public bool HasTransactionWithCategory(int categoryId)
        {
            var data = Data.Values.OfType<Transaction>().ToList();
            return data.Any(x => x.Category.Id == categoryId);
        }

        public bool HasTransactionWithAccount(int accountId)
        {
            var data = Data.Values.OfType<Transaction>().ToList();
            return data.Any(x => x.Account.Id == accountId);
        }
    }
}