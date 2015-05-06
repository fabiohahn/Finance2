using System;

namespace Finance
{
    public class Transaction : Entity
    {
        public decimal Value { get; protected set; }
        public DateTime Date { get; protected set; }
        public TransactionType TransactionType { get; protected set; }
        public Category Category { get; protected set; }
        public string Description { get; protected set; }
        public Account Account { get; protected set; }
        public Property Property { get; protected set; }

        public Transaction() { }

        public Transaction(decimal value, DateTime date, Category category, string description, Account account, Property property)
        {
            Validate(value, date, category, description, account, property);

            Value = value;
            Date = date;
            TransactionType = category.TransactionType;
            Category = category;
            Description = description;
            Account = account;
            Property = property;
        }

        private static void Validate(decimal value, DateTime date, Category category, string description, Account account, Property property)
        {
            if (value <= 0)
                throw new DomainException("Valor deve ser maior que zero");

            if (category == null)
                throw new DomainException("Categoria é obrigatória");

            if (account == null)
                throw new DomainException("Conta é obrigatória");

            if (property == null)
                throw new DomainException("Propriedade é obrigatória");

            if (property != category.Property)
                throw new DomainException("Propriedade da categoria é inválida");

            if (property != account.Property)
                throw new DomainException("Propriedade da conta é inválida");
        }

        public void UpdateCategory(Category category)
        {
            if(category.Property != Property)
                throw new DomainException("Propriedade da categoria é inválida");

            Category = category;
        }
    }
}
