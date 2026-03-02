namespace Infrastructure.Configuration
{
    public class ApiConfiguration : IConfiguration
    {
        public string JsonSectionName => "API";

        public string BaseUrl { get; set; }

        public string BaseCompanyUrl { get; set; }

        public int CompanyCreationTimeoutMilliseconds { get; set; }

        public string CompanyApiAccessKey { get; set; }
    }
}