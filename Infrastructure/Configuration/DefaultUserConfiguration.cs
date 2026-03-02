namespace Infrastructure.Configuration
{
    public class DefaultUserConfiguration : IConfiguration
    {
        public string JsonSectionName => "DefaultUser";

        public string Token { get; set; }
    }
}