namespace Infrastructure.Configuration
{
    public class UserConfiguration : IConfiguration
    {
        public string JsonSectionName => "User";

        public string TestMarker { get; set; }

        public string EmailPattern { get; set; }

        public string FirstNamePattern { get; set; }

        public string LastNamePattern { get; set; }

        public string Password { get; set; }
    }
}