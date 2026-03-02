namespace Infrastructure.Configuration
{
    public class SqlDatabaseConfiguration : IConfiguration
    {
        public string JsonSectionName => "SqlDatabase";

        public string ConnectionString { get; set; }

        public string Server { get; set; }

        public string MainDatabase { get; set; }

        public string User { get; set; }

        public string Password { get; set; }
    }
}