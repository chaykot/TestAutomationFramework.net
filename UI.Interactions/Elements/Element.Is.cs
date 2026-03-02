using System;
using UI.Interactions.Utils;

namespace UI.Interactions.Elements
{
    public partial class Element
    {
        public bool IsPresent()
        {
            try
            {
                Log.UiAction("Checking element is present".AddElementInfo(this));
                WrappedElement = null;
                return GetElement() != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsVisible()
        {
            try
            {
                Log.UiAction("Checking element is visible".AddElementInfo(this));
                return GetElement().Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsEnabled(bool wait = true)
        {
            if (wait)
            {
                this.WaitForElementVisible();
            }
            return GetElement().Enabled || !bool.TryParse(GetElement().GetAttribute("disabled"), out _);
        }
    }
}