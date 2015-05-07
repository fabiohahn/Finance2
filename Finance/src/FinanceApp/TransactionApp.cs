using System;
using App.Dtos;
using App.Helpers;
using Finance;
using Finance.IRepositories;

namespace App
{
    public class TransactionApp
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPropertyRepository _propertyRepository;

        public TransactionApp()
        {

        }

        public TransactionApp(ITransactionRepository transactionRepository, IAccountRepository accountRepository, ICategoryRepository categoryRepository, IPropertyRepository propertyRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _categoryRepository = categoryRepository;
            _propertyRepository = propertyRepository;
        }

        public TransactionListDto List(int propertyId, int accountId, int categoryId, DateTime referenceDate)
        {
            var transactions = _transactionRepository.GetAll(propertyId, referenceDate, accountId, categoryId);
            var accounts = _accountRepository.GetAll(propertyId);
            var categories = _categoryRepository.GetAll(propertyId);

            return new TransactionListDto(transactions, accounts, categories, accountId, categoryId, referenceDate);
        }

        public TransactionToSaveDto Show(int propertyId, int transactionId)
        {
            var categories = _categoryRepository.GetAll(propertyId);
            var accounts = _accountRepository.GetAll(propertyId);
            var transaction = _transactionRepository.Get(transactionId);

            return new TransactionToSaveDto(categories, accounts, transaction);
        }

        public void Save(int propertyId, TransactionToSaveDto transactionDto)
        {
            var property = _propertyRepository.Get(propertyId);
            var account = _accountRepository.Get(transactionDto.AccountId);
            var category = _categoryRepository.Get(transactionDto.CategoryId);

            var transaction = new Transaction(transactionDto.Value, transactionDto.Date.ToDate(), category, transactionDto.Description, account, property);

            if (transactionDto.Id != 0)
                _transactionRepository.Update(transaction, transactionDto.Id);
            else
                _transactionRepository.Add(transaction);
        }

        public void Remove(int propertyId, int id)
        {
            var transaction = _transactionRepository.Get(id);

            if (transaction.Property.Id != propertyId)
                throw new DomainException("Transacão não pode ser removida");

            _transactionRepository.Remove(transaction);
        }

        public void Transfer(TransferToSaveDto transferData, int propertyId)
        {
            var creditCategory = _categoryRepository.GetCreditTransferCategory(propertyId);
            var debitCategory = _categoryRepository.GetDebitTransferCategory(propertyId);
            var originAccount = _accountRepository.Get(transferData.OriginAccountId);
            var destinyAccount = _accountRepository.Get((transferData.DestinyAccountId));
            var property = _propertyRepository.Get(propertyId);

            var creditTransaction = new Transaction(transferData.Value, transferData.Date.ToDate(), creditCategory, transferData.Description, destinyAccount, property);
            var debitTransaction = new Transaction(transferData.Value, transferData.Date.ToDate(), debitCategory, transferData.Description, originAccount, property);

            _transactionRepository.Add(creditTransaction);
            _transactionRepository.Add(debitTransaction);
        }

        public TransferToSaveDto ShowTransferData(int propertyId)
        {
            var accounts = _accountRepository.GetAll(propertyId);
            return new TransferToSaveDto(accounts);
        }
    }
}
