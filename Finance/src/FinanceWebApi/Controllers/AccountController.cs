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
    public class AccountController : ControllerBase
    {
        private readonly AccountApp _accountApp;

        public AccountController()
        {
            var propertyRepository = RepositoryFactory.GetPropertyRepository();
            var accountRepository = RepositoryFactory.GetAccountRepository();
            var transactionRepository = RepositoryFactory.GetTransactionRepository();

            _accountApp = new AccountApp(accountRepository, propertyRepository, transactionRepository);
        }

        [HttpGet]
        public IList<AccountDto> Get()
        {
            return _accountApp.List(SeletedPropertyId);
        }

        [HttpGet("{id}")]
        public AccountDto Get(int id = 0)
        {
            return _accountApp.Show(SeletedPropertyId, id);
        }

        [HttpPost]
        public void Post([FromBody]AccountDto accountToSave)
        {
            _accountApp.Save(SeletedPropertyId, accountToSave);
        }
        
        [HttpPut("{id}")]
        public void Put([FromBody]AccountDto accountToSave)
        {
            _accountApp.Save(SeletedPropertyId, accountToSave);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _accountApp.Remove(SeletedPropertyId, id);
        }
    }
}
