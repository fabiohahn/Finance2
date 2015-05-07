using Finance;
using System;
using FinanceTest.Builder;
using Xunit;

namespace FinanceTest
{
    public class TransactionTest
    {
        private Property _property;
        private Account _account;
        private Category _category;

        public TransactionTest()
        {
            _property = PropertyBuilder.AProperty().Build();
            _account = AccountBuilder.AnAccount().WithProperty(_property).Build();
            _category = new Category("test", _property, TransactionType.Debit);
        }

        [Fact]
        public void ShouldCreateATransaction()
        {
            var transaction = new Transaction(10m, DateTime.Now, _category, "teste", _account, _property);
			
            Assert.NotNull(transaction);
        }

        [Fact]
        public void ValueShouldBeMoreThenZero()
        {
            var ex = Assert.Throws<DomainException>(() => new Transaction(-3m, new DateTime(), _category, "test", _account, _property));
            
			Assert.Equal("Valor deve ser maior que zero", ex.Message);
        }

        [Fact]
        public void ValueShouldntBeZero()
        {
            var ex = Assert.Throws<DomainException>(() => new Transaction(0, new DateTime(), _category, "test", _account, _property));
            
			Assert.Equal("Valor deve ser maior que zero", ex.Message);
        }

        [Fact]
        public void CategoryShouldNotBeNull()
        {
            var ex = Assert.Throws<DomainException>(() => new Transaction(3m, new DateTime(), null, "test", _account, _property));
            
			Assert.Equal("Categoria é obrigatória", ex.Message);
        }

        [Fact]
        public void AccountShouldNotBeNull()
        {
            var ex = Assert.Throws<DomainException>(() => new Transaction(3m, new DateTime(), _category, "test", null, _property));
            
			Assert.Equal("Conta é obrigatória", ex.Message);
        }

        [Fact]
        public void PropertyShouldNotBeNull()
        {
            var ex = Assert.Throws<DomainException>(() => new Transaction(3m, new DateTime(), _category, "test", _account, null));
            
			Assert.Equal("Propriedade é obrigatória", ex.Message);
        }

        [Fact]
        public void CategoryAndTransactionShoulBelongToTheSameProperty()
        {
            var property = PropertyBuilder.AProperty().WithId(2).Build();
            var category = CategoryBuilder.ACategory().WithProperty(property).Build();
            
            var ex = Assert.Throws<DomainException>(() => new Transaction(3m, DateTime.Now, category, "test", _account, _property));
            
			Assert.Equal("Propriedade da categoria é inválida", ex.Message);
        }

        [Fact]
        public void AccountAndTransactionShoulBelongToTheSameProperty()
        {
            var property = PropertyBuilder.AProperty().WithId(2).Build();
            var account = AccountBuilder.AnAccount().WithProperty(property).Build();
            
            var ex = Assert.Throws<DomainException>(() => new Transaction(3m, DateTime.Now, _category, "test", account, _property));
            
			Assert.Equal("Propriedade da conta é inválida", ex.Message);
        }

        [Fact]
        public void ShouldChangeACategory()
        {
            var category = CategoryBuilder.ACategory().WithId(2).Build();
            var transaction = TransactionBuilder.ATransaction().Build();

            transaction.UpdateCategory(category);

            Assert.Equal(category.Id, transaction.Category.Id);
        }

        [Fact]
        public void ShouldntChangeCategoryIfItIsFromAnotherProperty()
        {
            var property = PropertyBuilder.AProperty().WithId(2).Build();
            var newCategory = CategoryBuilder.ACategory().WithId(3).WithProperty(property).Build();
            var transaction = TransactionBuilder.ATransaction().WithProperty(_property).Build();

            var ex = Assert.Throws<DomainException>(() => transaction.UpdateCategory(newCategory));

			Assert.Equal("Propriedade da categoria é inválida", ex.Message);
        }
    }
}
