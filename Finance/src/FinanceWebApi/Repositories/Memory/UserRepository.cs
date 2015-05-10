using System.Linq;
using Finance;
using Finance.IRepositories;

namespace FinanceMvc.Repositories.Memory
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public User Get(string login, string hashedPassword)
        {
            var data = Data.Values.OfType<User>().ToList();
            return data.FirstOrDefault(x => x.Username == login && x.Password == hashedPassword);
        }
    }
}