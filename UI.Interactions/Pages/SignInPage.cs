using UI.Interactions.Elements;

namespace UI.Interactions.Pages
{
    public class SignInPage : BasePage
    {
        private readonly Element _emailInput = new("//input[@name = 'email']", "Email");
        private readonly Element _passwordInput = new("//input[@name = 'password']", "Password");
        private readonly Element _signInButton = new("//button[@type= 'submit']", "SignIn");

        public SignInPage() : base("//h2[text()='Sign in to your account']", "Sign in to your account")
        {
        }

        public void SetEmail(string email) => _emailInput.SetText(email);

        public void SetPassword(string password) => _passwordInput.SetText(password);

        public void ClickSignIn() => _signInButton.Click();
    }
}