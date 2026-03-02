using System.Collections.Generic;

namespace Infrastructure.Configuration
{
    public class CompanyConfiguration : IConfiguration
    {
        public string JsonSectionName => "Company";

        public string Name { get; set; }

        public List<int> MainCompanyIds { get; set; }

        public int Id { get; set; }

        public string Database { get; set; }
    }
}