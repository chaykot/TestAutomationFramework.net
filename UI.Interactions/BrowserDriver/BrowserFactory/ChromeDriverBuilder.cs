using Infrastructure.Configuration;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace UI.Interactions.BrowserDriver.BrowserFactory
{
    public class ChromeDriverBuilder : DriverBuilder
    {
        public override WebDriver GetDriver()
        {
            var chromeOptions = new ChromeOptions();
            if (Configuration.Browser.Headless)
            {
                chromeOptions.AddArguments("--headless");
            }
            var driverService = ChromeDriverService.CreateDefaultService(PathToDriverBinary);
            RemoteWebDriver remoteWebDriver = new ChromeDriver(driverService, chromeOptions);
            return new WebDriver { RemoteWebDriver = remoteWebDriver };
        }
    }
}