using System.Collections.Generic;

namespace Finance
{
    public class User : Entity
    {
        public string Name { get; protected set; }
        public string Username { get; protected set; }
        public string Password { get; protected set; }
        public Property FavoriteProperty { get; protected set; }
        public IList<Property> Properties { get; protected set; }
        
        public User(){}

        public User(string name, string username, string password, Property favoriteProperty)
        {
            Name = name;
            Username = username;
            Password = password;
            FavoriteProperty = favoriteProperty;
            
            Properties = new List<Property>();
        }

        public void ChangeFavoriteProperty(Property newProperty)
        {
            ValidateProperty(newProperty);
            

            FavoriteProperty = newProperty;
        }

        private void ValidateProperty(Property newProperty)
        {
            if (newProperty == null)
                throw new DomainException("Propriedade é obrigatória");
        }

        public void AddProperty(Property property)
        {
           Properties.Add(property); 
        }
    }
}
