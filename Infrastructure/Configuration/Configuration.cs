using System;
using System.Globalization;
using System.IO;
using Infrastructure.Configuration.Browser;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Configuration
{
    public static class Configuration
    {
        public static readonly IConfigurationRoot ConfigurationRoot = GetConfiguration();
        public static readonly BrowserConfiguration Browser = BindConfiguration<BrowserConfiguration>();
        public static readonly WaitConfiguration Wait = BindConfiguration<WaitConfiguration>();
        public static readonly SqlDatabaseConfiguration SqlDatabase = BindConfiguration<SqlDatabaseConfiguration>();
        public static readonly ApiConfiguration Api = BindConfiguration<ApiConfiguration>();
        public static readonly DefaultUserConfiguration DefaultUser = BindConfiguration<DefaultUserConfiguration>();
        public static readonly UserConfiguration User = BindConfiguration<UserConfiguration>();
        public static readonly CompanyConfiguration Company = BindConfiguration<CompanyConfiguration>();
        public static readonly bool LogDbData = bool.Parse(ConfigurationRoot["LogDbData"]);
        public static readonly bool MakeScreenshotOnFail = bool.Parse(ConfigurationRoot["MakeScreenshotOnFail"]);
        public static readonly CultureInfo CultureInfo = new(ConfigurationRoot["Locale"]);

        public static string GetSqlDbConnectionString(string dbName)
        {
            return string.Format(SqlDatabase.ConnectionString,
                SqlDatabase.Server,
                dbName,
                SqlDatabase.User,
                SqlDatabase.Password);
        }

        private static IConfigurationRoot GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile("appsettings.UI.json", true)
                .AddJsonFile("appsettings.API.json", true)
                .AddEnvironmentVariables()
                .Build();
        }

        public static T BindConfiguration<T>() where T : IConfiguration
        {
            var configuration = Activator.CreateInstance<T>();
            ConfigurationRoot.GetSection(configuration.JsonSectionName).Bind(configuration);
            return configuration;
        }
    }
}