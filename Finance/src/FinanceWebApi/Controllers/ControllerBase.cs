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
        protected static readonly IRepositoryFactory RepositoryFactory = new MemoryRepositoryFactory();
        protected readonly int SeletedPropertyId = 1;

        public ControllerBase()
        {
            DataBaseFiller.FillSampleData(RepositoryFactory);            
            SeletedPropertyId = 1;            
        }
    }
}