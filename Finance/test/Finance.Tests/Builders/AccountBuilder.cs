using Finance;

namespace FinanceTest.Builder
{
    public class AccountBuilder
    {
        private Property _property;
        private string _name;
        private int _id;

        public static AccountBuilder AnAccount()
        {
            return new AccountBuilder();
        }

        public Account Build()
        {
            if (_property == null)
                _property = new Property("Nova Propriedade Da Conta"){Id = _id};

            if (string.IsNullOrEmpty(_name))
                _name = "Nova Conta";

            return new Account(_name, _property);
        }

        public AccountBuilder WithProperty(Property property)
        {
            _property = property;
            return this;
        }

        public AccountBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public AccountBuilder WithId(int id)
        {
            _id = id;
            return this;
        }
    }
}