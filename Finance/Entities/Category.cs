namespace Finance
{
    public class Category : Entity
    {
        public string Name { get; protected set; }
        public Property Property { get; protected set; }
        public TransactionType TransactionType { get; protected set; }

        public Category() { }

        public Category(string name, Property property, TransactionType transactionType)
        {
            Name = name;
            Property = property;
            TransactionType = transactionType;
        }
    }
}
