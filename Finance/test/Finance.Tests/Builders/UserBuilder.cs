using Finance;

namespace FinanceTest.Builder
{
    public class UserBuilder
    {
        private string _name;
        private string _username;
        private string _password;
        private Property _favoriteProperty;
        private int _id;

        public static UserBuilder AnUser()
        {
            return new UserBuilder();
        }

        public UserBuilder WithProperty(Property property)
        {
            _favoriteProperty = property;
            return this;
        }

        public UserBuilder WithId(int id)
        {
            _id = id;
            return this;
        }
        
        public UserBuilder WithUsername(string username){
            _username = username;
            return this;
        }
        
        public UserBuilder WithPassword(string password)
        {
            _password = password;
            return this;
        }

        public User Build()
        {
            if (string.IsNullOrEmpty(_name))
                _name = "user";

            if (string.IsNullOrEmpty(_username))
                _username = "user";

            if (string.IsNullOrEmpty(_password))
                _password = "123";

            return new User(_name, _username, _password, _favoriteProperty) {Id = _id};
        }
    }
}