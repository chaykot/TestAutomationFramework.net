using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace UI.Interactions.Elements.CommonComponents.Grid
{
    public class Grid : Element
    {
        public ElementsCollection<GridRow> Rows => new(".//tbody/tr", $"rows of '{Name}'", this);
        private ElementsCollection<Element> ColumnNames => new(".//th/span", $"Column names of '{Name}'", this);

        public Grid(string locator, string name, Element parent = null) : base(locator, name, parent)
        {
        }

        public GridRow GetRow(Dictionary<string, string> conditions)
        {
            return GetAllRows().FirstOrDefault(row => row.MatchConditions(conditions));
        }

        public IEnumerable<GridRow> GetAllRows()
        {
            return Rows.GetElements();
        }

        protected internal int GetColumnNumberByName(string columnName)
        {
            var index = ColumnNames.GetTextViaAttribute().IndexOf(columnName);
            if (index == -1)
            {
                throw new NotFoundException($"Column with name '{columnName}' is not found");
            }
            return index;
        }
    }
}