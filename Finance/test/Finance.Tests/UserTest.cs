using Finance;
using FinanceTest.Builder;
using Xunit;

namespace FinanceTest
{
    public class UserTest
    {
        [Fact]
        public void ShouldChangeTheFavoriteProperty()
        {
            var property = PropertyBuilder.AProperty().WithName("propertyTest").Build();
            var newProperty = PropertyBuilder.AProperty().WithName("new property").Build();
            var user = UserBuilder.AnUser().WithProperty(property).Build();

            user.ChangeFavoriteProperty(newProperty);

            Assert.Equal(newProperty.Name, user.FavoriteProperty.Name);
        }

        [Fact]
        public void FavoritePropertyShouldntBeNull()
        {
            var user = UserBuilder.AnUser().Build();
            
            var exception = Assert.Throws<DomainException>(() => user.ChangeFavoriteProperty(null));

            Assert.Equal("Propriedade é obrigatória", exception.Message);
        }

        [Fact]
        public void ShouldAddAProperty()
        {
            var property = PropertyBuilder.AProperty().WithId(2).Build();
            var user = UserBuilder.AnUser().Build();

            user.AddProperty(property);

            Assert.True(user.Properties.Contains(property));
        }
    }
}
