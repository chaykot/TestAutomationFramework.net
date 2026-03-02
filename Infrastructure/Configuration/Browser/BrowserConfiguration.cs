namespace Infrastructure.Configuration.Browser
{
    public class BrowserConfiguration : IConfiguration
    {
        public string JsonSectionName => "Browser";

        public string StartUrl { get; set; }

        public BrowserType Type { get; set; }

        public bool Headless { get; set; }

        public int HeadlessWindowWidth { get; set; }

        public int HeadlessWindowHeight { get; set; }
    }
}