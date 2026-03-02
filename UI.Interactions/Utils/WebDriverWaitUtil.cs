using System;
using Infrastructure.Configuration;
using Infrastructure.Logger;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using UI.Interactions.BrowserDriver;
using UI.Interactions.Elements;

namespace UI.Interactions.Utils
{
    public static class WebDriverWaitUtil
    {
        private static readonly LogHelper Log = LogHelper.Instance;
        private static Browser Browser => Browser.Instance;

        public static void WaitForCondition(Func<IWebDriver, bool> condition, string message, string failMessage = null)
        {
            WaitForCondition(condition, Configuration.Wait.DefaultTimeout, message, failMessage);
        }

        public static Element WaitForElementPresent(this Element element)
        {

            WaitForCondition(_ => element.IsPresent(), $"{element.Name} present");
            return element;
        }

        public static Element WaitForElementVisible(this Element element)
        {
            WaitForCondition(_ => element.IsVisible(), $"{element.Name} visible");
            return element;
        }

        public static Element WaitForElementInvisible(this Element element)
        {
            WaitForCondition(_ => !element.IsVisible(), $"{element.Name} invisible");
            return element;
        }

        public static void WaitForPageToLoad(this Browser browser)
        {
            WaitForCondition(_ => browser.IsPageLoaded(), Configuration.Wait.PageLoadTimeout, "page loaded");
        }

        private static void WaitForCondition(
            Func<IWebDriver, bool> condition,
            double timeoutSeconds,
            string conditionDescription,
            string failMessage = null)
        {
            try
            {
                new WebDriverWait(Browser.Driver, TimeSpan.FromSeconds(timeoutSeconds)).Until(condition);
            }
            catch (Exception)
            {
                Log.Error($"Condition '{conditionDescription}' wasn't achieved in {timeoutSeconds} seconds. {failMessage}");
            }
        }
    }
}