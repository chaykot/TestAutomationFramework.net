using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace UI.Interactions.Elements.CommonComponents.Grid
{
    public class GridRow : Element
    {
        private Grid Grid { get; }

        public GridRow(IWebElement webElement, string locator, string name, Grid grid)
            : base(webElement, locator, name, grid)
        {
            Grid = grid;
        }

        public GridRow(string locator, string name, Grid grid) : base(locator, name, grid)
        {
            Grid = grid;
        }

        public ElementsCollection<GridCell> Cells => new(".//td", "cell", this);

        public GridCell GetCellAtColumn(string columnName)
        {
            return Cells.GetElements().ElementAt(Grid.GetColumnNumberByName(columnName));
        }

        public bool MatchConditions(Dictionary<string, string> conditions)
        {
            return conditions.All(condition => GetCellAtColumn(condition.Key).GetText() == condition.Value);
        }
    }
}