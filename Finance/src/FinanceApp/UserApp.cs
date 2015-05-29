using Finance.IRepositories;

namespace App
{
    public class UserApp
    {
        private readonly IUserRepository _userRepository;
        private readonly IPropertyRepository _propertyRepository;

        public UserApp(IUserRepository userRepository, IPropertyRepository propertyRepository)
        {
            _userRepository = userRepository;
            _propertyRepository = propertyRepository;
        }

        public void ChangeFavoriteProperty(int userId, int newPropertyId)
        {
            var property = _propertyRepository.Get(newPropertyId);
            var user = _userRepository.Get(userId);

            user.ChangeFavoriteProperty(property);

            _userRepository.Update(user, user.Id);
        }
        
        public int GetPropertyOfUser(string username, string password)
        {
            var hashedPassword = Hash.GetHash(password);
            var user = _userRepository.Get(username, hashedPassword);
            
            if(user != null)
                return user.FavoriteProperty.Id;
            return 0;
        }
    }
}