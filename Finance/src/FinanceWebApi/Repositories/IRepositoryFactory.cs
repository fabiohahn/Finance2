using Finance.IRepositories;

namespace FinanceMvc.Repositories
{
    public interface IRepositoryFactory
    {
        ICategoryRepository GetCategoryRepository();
        IPropertyRepository GetPropertyRepository();
        ITransactionRepository GetTransactionRepository();
        IAccountRepository GetAccountRepository();
        IUserRepository GetUserRepository();
    }
}