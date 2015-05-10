using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using FinanceMvc.Repositories;
using FinanceMvc.Repositories.Memory;
 
namespace FinanceWebApi.Controllers
{
    public class ControllerBase : Controller
    {
        protected readonly IRepositoryFactory RepositoryFactory;
        protected readonly int SeletedPropertyId = 0;

        public ControllerBase()
        {
            RepositoryFactory = new MemoryRepositoryFactory();
            DataBaseFiller.FillSampleData(RepositoryFactory);
            
            SeletedPropertyId = 1;            
        }
    }
}