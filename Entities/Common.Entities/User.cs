namespace Entities.Common.Entities
{
    public class User
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public User(string email, string password, string name = null)
        {
            Email = email;
            Password = password;
            Name = name;
        }

        public static User GetRandomUser()
        {
            return new User(Faker.User.Email(), Faker.User.Password(), Faker.User.Username());
        }
    }
}
