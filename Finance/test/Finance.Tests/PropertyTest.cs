using Finance;
using Xunit;

namespace FinanceTest
{
    public class PropertyTest
    {
        [Fact]
        public void ShouldCreateAProperty()
        {
            const string propertyName = "Property test";
            var property = new Property(propertyName);

            Assert.Equal(propertyName, property.Name);
        }
    }
}