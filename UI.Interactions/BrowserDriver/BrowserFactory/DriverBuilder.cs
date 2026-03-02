using System.IO;

namespace UI.Interactions.BrowserDriver.BrowserFactory
{
    public abstract class DriverBuilder
    {
        protected string PathToDriverBinary { get; } = Path.GetDirectoryName(typeof(DriverBuilder).Assembly.Location);

        public abstract WebDriver GetDriver();
    }
}