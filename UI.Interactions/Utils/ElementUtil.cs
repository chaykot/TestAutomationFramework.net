using System.Text;
using UI.Interactions.Elements;

namespace UI.Interactions.Utils
{
    public static class ElementUtil
    {
        public static string AddElementInfo(this string message, Element element)
        {
            var elementInfo = new StringBuilder()
                .Append(message)
                .Append($" '{element.Name}' with locator '{element.Locator}' ")
                .Append(element.Parent != null
                    ? $"with parent element{AddElementInfo(string.Empty, element.Parent)}"
                    : string.Empty)
                .ToString();
            return $"{elementInfo}";
        }
    }
}