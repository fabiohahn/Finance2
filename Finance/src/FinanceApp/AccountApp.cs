using System.Collections.Generic;
using System.Linq;
using App.Dtos;
using Finance;
using Finance.IRepositories;

namespace App
{
    public class AccountApp
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly ITransactionRepository _transactionRepository;

        public AccountApp(IAccountRepository accountRepository, IPropertyRepository propertyRepository, ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _propertyRepository = propertyRepository;
            _transactionRepository = transactionRepository;
        }

        public IList<AccountDto> List(int propertyId)
        {
            var accounts = _accountRepository.GetAll(propertyId);

            return accounts.Select(account => new AccountDto(account)).ToList();
        }

        public AccountDto Show(int propertyId, int accountId)
        {
            var account = _accountRepository.Get(accountId);

            return new AccountDto(account);
        }

        public void Save(int propertyId, AccountDto accountToSave)
        {
            var property = _propertyRepository.Get(propertyId);

            var account = new Account(accountToSave.Name, property);

            if (accountToSave.Id != 0)
                _accountRepository.Update(account, accountToSave.Id);
            else
                _accountRepository.Add(account);
        }

        public void Remove(int propertyId, int id)
        {
            var account = _accountRepository.Get(id);
            var haveTransaction = _transactionRepository.HasTransactionWithAccount(account.Id);

            if (haveTransaction)
                throw new DomainException("Conta está registrada em uma transação");

            if (account.Property.Id != propertyId)
                throw new DomainException("Conta não pode ser removida");

            _accountRepository.Remove(account);
        }
    }
}
