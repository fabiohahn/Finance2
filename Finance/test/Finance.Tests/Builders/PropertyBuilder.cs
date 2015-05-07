using Finance;

namespace FinanceTest.Builder
{
    public class PropertyBuilder
    {
        private int _id;
        private string _name;

        public static PropertyBuilder AProperty()
        {
            return new PropertyBuilder();
        }

        public PropertyBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public PropertyBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public Property Build()
        {
            if (string.IsNullOrEmpty(_name))
                _name = "Nova Property";

            var property = new Property(_name) {Id = _id};

            return property;
        }
    }
}
