using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using App;
using App.Dtos;

namespace FinanceWebApi.Controllers
{
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionApp _transactionApp;
        private readonly CategoryChangerApp _categoryChangerApp;

        public TransactionController()
        {
            var accountRepository = RepositoryFactory.GetAccountRepository();
            var transactionRepository = RepositoryFactory.GetTransactionRepository();
            var categoryRepository = RepositoryFactory.GetCategoryRepository();
            var propertyRepository = RepositoryFactory.GetPropertyRepository();

            _transactionApp = new TransactionApp(transactionRepository, accountRepository, categoryRepository, propertyRepository);
            _categoryChangerApp = new CategoryChangerApp(transactionRepository, categoryRepository);
        }
        
        [HttpGet]
        public TransactionListDto Get(int month = 0, int year = 0, int accountId = 0, int categoryId = 0)
        {
            if (month == 0)
                month = DateTime.Today.Month;
            if (year == 0)
                year = DateTime.Today.Year;

            return _transactionApp.List(SeletedPropertyId, accountId, categoryId, new DateTime(year, month, 1));
        }

        [HttpGet("{id}")]
        public TransactionToSaveDto Get( int id)
        {
             return _transactionApp.Show(SeletedPropertyId, id);
        }

        [HttpPost]
        public void Post([FromBody]TransactionToSaveDto transaction)
        {
            _transactionApp.Save(SeletedPropertyId, transaction);            
        }

        [HttpPut("{id}")]
        public void Put([FromBody]TransactionToSaveDto transaction)
        {
            _transactionApp.Save(SeletedPropertyId, transaction);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _transactionApp.Remove(SeletedPropertyId, id);
        }
        
        [HttpGet("transfer")]
        public TransferToSaveDto Transfer()
        {
            return _transactionApp.ShowTransferData(SeletedPropertyId);
        }

        [HttpPost("transfer")]
        public void Transfer([FromBody]TransferToSaveDto transferData)
        {
            _transactionApp.Transfer(transferData, SeletedPropertyId);
        }

        [HttpPost]
        public void ChangeCategory(int categoryId, [FromBody]List<int> transactionIds)
        {
            _categoryChangerApp.ChangeCategories(transactionIds, categoryId);
        }
    }
}
