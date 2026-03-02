using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using UI.Interactions.Utils;

namespace UI.Interactions.Elements
{
    public partial class Element
    {
        public void Clear()
        {
            this.WaitForElementVisible();
            Log.UiAction("Clear".AddElementInfo(this));
            GetElement().Clear();
        }

        /// <summary>
        /// Type text without clearing the old one.
        /// </summary>
        public void Type(string text, bool waitForVisible = true)
        {
            if (waitForVisible)
            {
                this.WaitForElementVisible();
            }
            Log.UiAction($"Type '{text}' in".AddElementInfo(this));
            GetElement().SendKeys(text);
        }

        /// <summary>
        /// Clear input field and set new text using standard selenium methods.
        /// </summary>
        public void SetText(string text)
        {
            if (text == null)
            {
                return;
            }
            this.WaitForElementVisible();
            Log.UiAction($"Set '{text}' in".AddElementInfo(this));
            GetElement().Clear();
            GetElement().SendKeys(text);
        }


        /// <summary>
        /// Clear input field and set new text using Actions methods.
        /// </summary>
        public void SetTextViaActions(string text)
        {
            if (text == null)
            {
                return;
            }
            this.WaitForElementVisible();
            ((IJavaScriptExecutor)Browser.Driver).ExecuteScript("arguments[0].focus()", GetElement());
            Actions actions = new Actions(Browser.Driver);
            actions.KeyDown(Keys.Control).SendKeys("a").KeyUp(Keys.Control);
            actions.SendKeys(Keys.Delete);
            actions.Build().Perform();
            if (!string.IsNullOrEmpty(text))
            {
                new Actions(Browser.Driver).SendKeys(text).Perform();
            }
        }
    }
}