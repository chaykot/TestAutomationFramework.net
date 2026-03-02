using FluentAssertions;
using UI.Interactions.BrowserDriver;
using UI.Interactions.Elements;
using UI.Interactions.Utils;

namespace UI.Interactions.Pages
{
    public abstract class BasePage
    {
        protected static Browser Browser => Browser.Instance;

        protected Element RootElement { get; }

        protected string Name { get; }

        protected BasePage(string rootElementLocator, string name)
        {
            Browser.WaitForPageToLoad();
            Name = name;
            RootElement = new Element(rootElementLocator, $"{name} Page");
        }

        public void AssertOpened()
        {
            RootElement.WaitForElementPresent();
            RootElement.IsVisible()
                .Should()
                .BeTrue($"'{Name}' page should be opened with root element for".AddElementInfo(RootElement));
        }

        public void AssertClosed()
        {
            RootElement.WaitForElementInvisible();
            RootElement.IsVisible()
                .Should()
                .BeFalse($"'{Name}' page should be closed with root element for".AddElementInfo(RootElement));
        }
    }
}