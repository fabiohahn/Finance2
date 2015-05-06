namespace Finance
{
    public class Property : Entity
    {   
        public string Name { get; protected set; }
        
        public Property()
        {
        }

        public Property(string name)
        {
            Name = name;
        }
    }
}
