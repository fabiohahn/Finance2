namespace Finance.IRepositories
{
    public interface IUserRepository
    {
        void Add(User user);
        void Update(User user, int id);
        User Get(string login, string hashedPassword);
        User Get(int userId);
    }
}