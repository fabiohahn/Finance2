using System;
using Finance;

namespace FinanceTest.Builder
{
    public class TransactionBuilder
    {
        private decimal _value;
        private TransactionType _type;
        private DateTime _date = DateTime.Today;
        private Category _category;
        private Account _account;
        private Property _property;
        private string _description;
        private int _id;

        public static TransactionBuilder ATransaction()
        {
            return new TransactionBuilder();
        }

        public TransactionBuilder WithValue(decimal value)
        {
            _value = value;
            return this;
        }

        public TransactionBuilder WithType(TransactionType type)
        {
            _type = type;
            return this;
        }

        public TransactionBuilder WithDate(DateTime date)
        {
            _date = date;
            return this;
        }

        public TransactionBuilder WithCategory(Category category)
        {
            _category = category;
            return this;
        }

        public TransactionBuilder WithAccount(Account account)
        {
            _account = account;
            return this;
        }

        public TransactionBuilder WithProperty(Property property)
        {
            _property = property;
            return this;
        }

        public TransactionBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public TransactionBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public Transaction Build()
        {
            if (_value == 0)
                _value = 10m;

            if (_property == null)
                _property = PropertyBuilder.AProperty().Build();

            if (_category == null)
                _category = CategoryBuilder.ACategory().WithTransactiontype(_type).WithProperty(_property).Build();

            if (_account == null)
                _account = AccountBuilder.AnAccount().WithProperty(_property).Build();

            if (string.IsNullOrEmpty(_description))
                _description = "test";

            var transaction = new Transaction(_value, _date, _category, _description, _account, _property) {Id = _id};

            return transaction;
        }
    }
}
