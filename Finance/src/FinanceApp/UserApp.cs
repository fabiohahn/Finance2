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
    }
}