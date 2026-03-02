using System;
using Infrastructure.Configuration;
using Infrastructure.Logger;
using UI.Interactions.Utils;

namespace UI.Interactions.Elements.CommonComponents
{
    public class DatePicker : Element
    {
        private readonly Element _monthAndYear =
            new("//div[contains(@class, 'CalendarHeader')]/p", "Date Picker: Month and Year Header");
        private readonly Element _next =
            new("//div[contains(@class, 'CalendarHeader')]/following-sibling::button", "Date Picker: Next");
        private readonly Element _previous =
            new("//div[contains(@class, 'CalendarHeader')]/preceding-sibling::button", "Date Picker: Previous");
        private readonly Element _monthChangeSlider = 
            new ("//div[contains(@class, 'slideEnterActive')]", "Date Picker: Month Change Activity");
        private const string DayLocator = "//button[not(contains(@class, 'hidden'))]//p[text()='{0}']";

        public DatePicker(string locator, string name, Element parent = null) : base(locator, name, parent)
        {
        }

        public void SetDate(DateTime date)
        {
            SelectMonthAndYear(date);
            SelectDay(date);
        }

        public string GetMonthAndYear()
        {
            return _monthAndYear.GetText();
        }

        private void SelectMonthAndYear(DateTime expectedDate)
        {
            var actualMonthAndYearString = GetMonthAndYear();
            var actualMonthAndYearDate = DateTime.Parse(actualMonthAndYearString);
            var expectedMonthAndYearString = expectedDate.ToString("MMMM yyyy", Configuration.CultureInfo);
            LogHelper.Instance.UiAction($"Searching for {expectedMonthAndYearString}");
            if (expectedDate.Month == actualMonthAndYearDate.Month && expectedDate.Year == actualMonthAndYearDate.Year)
            {
                return;
            }
            if (expectedDate.Month < actualMonthAndYearDate.Month && expectedDate.Year <= actualMonthAndYearDate.Year 
                || expectedDate.Year < actualMonthAndYearDate.Year)
            {
                while (!GetMonthAndYear().Equals(expectedMonthAndYearString))
                {
                    _previous.Click();
                }
            }
            if (expectedDate.Month > actualMonthAndYearDate.Month && expectedDate.Year >= actualMonthAndYearDate.Year
                || expectedDate.Year > actualMonthAndYearDate.Year)
            {
                while (!GetMonthAndYear().Equals(expectedMonthAndYearString))
                {
                    _next.Click();
                }
            }
        }

        private void SelectDay(DateTime expectedDate)
        {
            var day = expectedDate.Day;
            _monthChangeSlider.WaitForElementInvisible();
            var dayElement = new Element(string.Format(DayLocator, expectedDate.Day), $"Date Picker: Day {day}");
            dayElement.Click();
        }
    }
}