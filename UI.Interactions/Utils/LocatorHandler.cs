using System;
using OpenQA.Selenium;

namespace UI.Interactions.Utils
{
    public static class LocatorHandler
    {
        public enum LocatorType
        {
            Xpath,
            CssSelector
        }

        public static By GetSeleniumBy(string locator)
        {
            var locatorType = GetLocatorType(locator);
            return locatorType switch
            {
                LocatorType.Xpath => By.XPath(locator),
                LocatorType.CssSelector => By.CssSelector(locator),
                _ => throw new NotImplementedException($"Cannot find locator type '{locatorType}'")
            };
        }

        private static LocatorType GetLocatorType(string locator)
        {
            if (locator.StartsWith("//", StringComparison.OrdinalIgnoreCase) ||
                locator.StartsWith(".//", StringComparison.OrdinalIgnoreCase) ||
                locator.StartsWith("./", StringComparison.OrdinalIgnoreCase) ||
                locator.StartsWith("(//", StringComparison.OrdinalIgnoreCase) ||
                locator.StartsWith("(.//", StringComparison.OrdinalIgnoreCase) ||
                locator.StartsWith("(./", StringComparison.OrdinalIgnoreCase))
            {
                return LocatorType.Xpath;
            }
            return LocatorType.CssSelector;
        }
    }
}