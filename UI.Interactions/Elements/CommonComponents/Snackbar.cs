namespace UI.Interactions.Elements.CommonComponents
{
    public class Snackbar : Element
    {
        private const string DefaultLocator = "//span[@data-test='snackbar']//span[text()='{0}']";
        public string Text { get; }

        public Snackbar(string text, Element parent = null)
            : base(string.Format(DefaultLocator, text), $"Snackbar with text '{text}'", parent)
        {
            Text = text;
        }
    }
}