using Finance;

namespace FinanceTest.Builder
{
    public class CategoryBuilder
    {
        private int _id;
        private Property _property;
        private TransactionType _type;
        private string _name;

        public static CategoryBuilder ACategory()
        {
            return new CategoryBuilder();
        }

        public Category Build()
        {
            if (_property == null)
                _property = PropertyBuilder.AProperty().Build();

            if (string.IsNullOrEmpty(_name))
                _name = "Nome da categoria";

            return new Category(_name,_property, _type){Id = _id};
        }

        public CategoryBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public CategoryBuilder WithProperty(Property property)
        {
            _property = property;
            return this;
        }

        public CategoryBuilder WithTransactiontype(TransactionType type)
        {
            _type = type;
            return this;
        }

        public CategoryBuilder WithId(int id)
        {
            _id = id;
            return this;
        }
    }
}