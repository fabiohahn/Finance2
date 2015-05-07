using App;
using Finance;
using Finance.IRepositories;
using FinanceTest.Builder;
using Moq;
using Xunit;

namespace FinanceAppTest.Tests
{
    public class UserAppTest
    {
        private UserApp _userApp;
        private Mock<IUserRepository> _userRepository;
        private Mock<IPropertyRepository> _propertyRepository;

        public UserAppTest()
        {
            _userRepository = new Mock<IUserRepository>();
            _propertyRepository = new Mock<IPropertyRepository>();
            _userApp = new UserApp(_userRepository.Object, _propertyRepository.Object);
        }

        [Fact]
        public void ShouldChangeTheUsersProperty()
        {
            const int userId = 2;
            const int newPropertyId = 3;
            var user = UserBuilder.AnUser().WithId(userId).Build();
            var property = PropertyBuilder.AProperty().WithId(newPropertyId).Build();
            _userRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(user);
            _propertyRepository.Setup(x => x.Get(newPropertyId)).Returns(property);

            _userApp.ChangeFavoriteProperty(user.Id, newPropertyId);

            _userRepository.Verify(x => x.Update(It.Is<User>(y => y.FavoriteProperty.Id == newPropertyId), userId));
        }
    }
}
