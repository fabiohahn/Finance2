using System;
using System.Collections.Generic;

namespace Finance.IRepositories
{
    public interface ITransactionRepository
    {
        IList<Transaction> GetAllThatAreNotTransfer(int propertyId, DateTime initialDate, DateTime finalDate);
        IList<Transaction> GetAll(int propertyId, DateTime date, int accountId, int categoryId);
        IList<Transaction> GetAll(int propertyId, DateTime initialDate, DateTime finalDate);
        IList<Transaction> GetAll(int propertyId);
        Transaction Get(int transactionId);
        void Update(Transaction transaction, int id);
        void Add(Transaction transaction);
        void Remove(Transaction transaction);
        bool HasTransactionWithCategory(int categoryId);
        bool HasTransactionWithAccount(int accountId);
        void Dispose();
    }
}