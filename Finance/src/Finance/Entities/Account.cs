namespace Finance
{
    public class Account : Entity
    {
        public string Name { get; protected set; }
        public Property Property { get; protected set; }

        public Account()
        {
            
        }

        public Account(string name, Property property)
        {
            Name = name;
            Property = property;
        }
    }
}
