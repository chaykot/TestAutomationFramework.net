using System;
using System.Collections.Generic;
using System.Linq;
using UI.Interactions.Utils;

namespace UI.Interactions.Elements
{
    public class ElementsCollection<T> : Element where T : Element
    {
        public ElementsCollection(string locator, string name = null, Element parent = null)
            : base(locator, name, parent)
        {
        }

        public IEnumerable<T> GetElements(bool wait = true)
        {
            if (wait)
            {
                ((T)Activator.CreateInstance(typeof(T), Locator, Name, Parent)).WaitForElementPresent();
            }
            return GetSearchContext()
                .FindElements(LocatorHandler.GetSeleniumBy(Locator))
                ?.Select(element => (T)Activator.CreateInstance(typeof(T), element, Locator, Name, Parent));
        }

        public new List<string> GetText(bool waitForEachElement = false)
        {
            return GetElements().Select(element => element.GetText(waitForEachElement)).ToList();
        }

        public new List<string> GetTextViaAttribute(bool waitForEachElement = false)
        {
            return GetElements().Select(element => element.GetTextViaAttribute(waitForEachElement)).ToList();
        }
    }
}