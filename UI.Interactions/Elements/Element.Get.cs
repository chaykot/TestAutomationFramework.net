using System;
using UI.Interactions.Utils;

namespace UI.Interactions.Elements
{
    public partial class Element
    {
        public string GetText(bool wait = true)
        {
            if (wait)
            {
                this.WaitForElementVisible();
            }
            var text = GetElement().Text;
            Log.UiAction($"Text: '{text}' in ".AddElementInfo(this));
            return text;
        }

        public string GetTextViaAttribute(bool wait = true)
        {
            return GetAttribute("textContent", wait).Trim();
        }

        public string GetInputValue()
        {
            return GetAttribute("value");
        }

        public string GetAttribute(string attribute, bool wait = true)
        {
            if (wait)
            {
                this.WaitForElementVisible();
            }
            var value = GetElement().GetAttribute(attribute);
            Log.UiAction($"Attribute '{attribute}' = '{value}' in".AddElementInfo(this));
            return value;
        }

        public string TryGetAttribute(string attribute)
        {
            try
            {
                return GetAttribute(attribute);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}