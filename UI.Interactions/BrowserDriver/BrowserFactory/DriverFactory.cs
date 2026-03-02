using System.Collections.Generic;
using Infrastructure.Configuration;
using Infrastructure.Configuration.Browser;

namespace UI.Interactions.BrowserDriver.BrowserFactory
{
    public static class DriverFactory
    {
        private static readonly Dictionary<BrowserType, DriverBuilder> BrowserDictionary = new()
        {
            { BrowserType.Chrome, new ChromeDriverBuilder() }
        };

        public static WebDriver GetDriver()
        {
            return BrowserDictionary[Configuration.Browser.Type].GetDriver();
        }
    }
}