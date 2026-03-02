using System.Drawing;
using System.Threading;
using Infrastructure.Configuration;
using Infrastructure.Logger;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using UI.Interactions.BrowserDriver.BrowserFactory;

namespace UI.Interactions.BrowserDriver
{
    public class Browser
    {
        private static readonly ThreadLocal<Browser> BrowserInstances = new();
        private static readonly LogHelper Log = LogHelper.Instance;

        public static Browser Instance => GetBrowser();

        public RemoteWebDriver Driver { get; private set; }

        private Browser()
        {
            Log.Info($"Open browser: {Configuration.Browser.Type}{(Configuration.Browser.Headless ? " (headless)" : string.Empty)}");
            WebDriver webDriver = DriverFactory.GetDriver();
            Driver = webDriver.RemoteWebDriver;
        }

        public void MaximizeWindow()
        {
            Log.Info("Maximize browser window");
            if (Configuration.Browser.Headless)
            {
                SetWindowSize(Configuration.Browser.HeadlessWindowWidth, Configuration.Browser.HeadlessWindowHeight);
                return;
            }
            Driver.Manage().Window.Maximize();
        }

        public void Navigate(string url)
        {
            Log.Info($"Navigate to url: {url}");
            Driver.Navigate().GoToUrl(url);
        }

        public void OpenStartUrl(string startUrl)
        {
            Navigate(startUrl);
        }

        public void SetWindowSize(int width, int height)
        {
            Log.Info($"Set window size: {width} x {height}");
            Driver.Manage().Window.Size = new Size(width, height);
        }

        public void Refresh()
        {
            Log.Info("Refreshing Browser");
            Driver.Navigate().Refresh();
        }

        public bool IsPageLoaded()
        {
            return ((IJavaScriptExecutor)Driver).ExecuteScript("return document.readyState").Equals("complete");
        }

        public void Close()
        {
            Driver.Close();
            Driver.Quit();
            BrowserInstances.Value = null;
            Driver = null;
            Log.Info($"Browser '{Configuration.Browser.Type}' was closed");
        }

        private static Browser GetBrowser()
        {
            return BrowserInstances.Value ?? (BrowserInstances.Value = new Browser());
        }
    }
}