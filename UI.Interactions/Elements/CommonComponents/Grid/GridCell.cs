using OpenQA.Selenium;

namespace UI.Interactions.Elements.CommonComponents.Grid
{
    public class GridCell : Element
    {
        public GridCell(IWebElement element, string locator, string name, GridRow gridRow)
            : base(element, locator, name, gridRow)
        {
        }

        public GridCell(string locator, string name, GridRow gridRow) : base(locator, name, gridRow)
        {
        }
    }
}