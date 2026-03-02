namespace Infrastructure.Configuration
{
    public class WaitConfiguration : IConfiguration
    {
        public string JsonSectionName => "Wait";

        public int PageLoadTimeout { get; set; }

        public int DefaultTimeout { get; set; }
    }
}