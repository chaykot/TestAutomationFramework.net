using System;
using System.Linq;
using Infrastructure.Logger;
using OpenQA.Selenium;
using UI.Interactions.BrowserDriver;
using UI.Interactions.Utils;

namespace UI.Interactions.Elements
{
    public partial class Element
    {
        public string Locator { get; }

        public string Name { get; }

        public Element Parent { get; }

        protected IWebElement WrappedElement { get; set; }

        protected LogHelper Log = LogHelper.Instance;

        protected Browser Browser = Browser.Instance;

        public Element(string locator, string name, Element parent = null)
        {
            Locator = locator;
            Name = name;
            Parent = parent;
        }

        public Element(IWebElement element, string locator, string name, Element parent = null)
        {
            WrappedElement = element;
            Locator = locator;
            Name = name;
            Parent = parent;
        }

        public void ScrollIntoView()
        {
            Log.UiAction("Scroll to".AddElementInfo(this));
            ((IJavaScriptExecutor)Browser.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", GetElement());
        }

        protected IWebElement GetElement()
        {
            if (WrappedElement != null)
            {
                return WrappedElement;
            }
            var element = GetSearchContext()
                .FindElements(LocatorHandler.GetSeleniumBy(Locator))
                .FirstOrDefault();
            return element ?? throw new ArgumentException($"Cannot find element '{Name}' with locator '{Locator}'");
        }

        protected ISearchContext GetSearchContext()
        {
            return Parent == null
                ? Browser.Driver
                : Parent.GetElement();
        }
    }
}