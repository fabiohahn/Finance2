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
    public class CategoryController : ControllerBase
    {
        private readonly CategoryApp _categoryApp;
        
        public CategoryController()
        {
            var categoryRepository = RepositoryFactory.GetCategoryRepository();
            var propertyRepository = RepositoryFactory.GetPropertyRepository();
            var transactionRepository = RepositoryFactory.GetTransactionRepository();
            
            _categoryApp = new CategoryApp(categoryRepository, propertyRepository, transactionRepository);
        }

        [HttpGet]
        public IList<CategoryDto> Get()
        {
            return _categoryApp.List(SeletedPropertyId);
        }      

        [HttpGet("{id}")]
        public CategoryDto Get(int id = 0)
        {
            return _categoryApp.Show(SeletedPropertyId, id);
        }

        [HttpPost]
        public void Post([FromBody]CategoryDto categoryDto)
        {
            _categoryApp.Save(SeletedPropertyId, categoryDto);
        }
        
        [HttpPut("{id}")]
        public void Put([FromBody]CategoryDto categoryDto)
        {
            _categoryApp.Save(SeletedPropertyId, categoryDto);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _categoryApp.Remove(SeletedPropertyId, id);
        }
    }
}
