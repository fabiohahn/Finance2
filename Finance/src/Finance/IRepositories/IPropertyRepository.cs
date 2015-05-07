using System.Collections.Generic;

namespace Finance.IRepositories
{
    public interface IPropertyRepository
    {
        void Add(Property property);
        Property Get(int id);
        IList<Property> GetAll();
    }
}