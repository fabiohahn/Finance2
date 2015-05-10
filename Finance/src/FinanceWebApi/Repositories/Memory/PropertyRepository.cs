using System.Collections.Generic;
using System.Linq;
using Finance;
using Finance.IRepositories;

namespace FinanceMvc.Repositories.Memory
{
    public class PropertyRepository : RepositoryBase<Property>, IPropertyRepository
    {
        public IList<Property> GetAll()
        {
            return Data.Values.OfType<Property>().ToList();
        }
    }
}