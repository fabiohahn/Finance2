using System;
using Finance;

namespace FinanceMvc.Repositories.Memory
{
    public class DataBaseFiller
    {
        public static void FillSampleData(IRepositoryFactory repositoryFactory)
        {
            var propertyRepository = repositoryFactory.GetPropertyRepository();
            var userRepository = repositoryFactory.GetUserRepository();
            var accountRepository = repositoryFactory.GetAccountRepository();
            var categoryRepository = repositoryFactory.GetCategoryRepository();
            var transactionRepository = repositoryFactory.GetTransactionRepository();

            var property = new Property("property");
            var user = new User("name", "username", "QL0AFWMIX8NRZTKeof9cXsvbvu8=", property);
            var account = new Account("account", property);
            var creditCategory = new Category("credit", property, TransactionType.Credit);
            var creditTransferCategory = new Category("credit transfer", property, TransactionType.CreditTransfer);
            var debitCategory = new Category("debit", property, TransactionType.Debit);
            var debitTransferCategory = new Category("debit transfer", property, TransactionType.DebitTransfer);
            var creditTransaction = new Transaction(40, DateTime.Today, creditCategory, "", account, property);
            var debitTransaction = new Transaction(10, DateTime.Today, debitCategory, "", account, property);

            if(propertyRepository.Get(1) != null)
                return;

            propertyRepository.Add(property);
            userRepository.Add(user);
            accountRepository.Add(account);
            categoryRepository.Add(creditCategory);
            categoryRepository.Add(creditTransferCategory);
            categoryRepository.Add(debitCategory);
            categoryRepository.Add(debitTransferCategory);
            transactionRepository.Add(creditTransaction);
            transactionRepository.Add(debitTransaction);
        }
    }
}