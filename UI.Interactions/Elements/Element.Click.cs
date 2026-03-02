using UI.Interactions.Utils;

namespace UI.Interactions.Elements
{
    public partial class Element
    {
        public virtual void Click()
        {
            this.WaitForElementVisible();
            Log.UiAction("Click".AddElementInfo(this));
            GetElement().Click();
        }

        public virtual void ClickWithScroll()
        {
            this.WaitForElementVisible();
            ScrollIntoView();
            Log.UiAction("Click".AddElementInfo(this));
            GetElement().Click();
        }

        public void ClickUsingJs()
        {
            this.WaitForElementVisible();
            Log.UiAction("Click using JS".AddElementInfo(this));
            Browser.Driver.ExecuteScript("arguments[0].click();", GetElement());
        }
    }
}