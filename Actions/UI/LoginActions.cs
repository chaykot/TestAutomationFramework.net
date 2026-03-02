using Entities.Common.Entities;
using UI.Interactions.Pages;

namespace Actions.UI
{
    public static class LoginActions
    {
        public static void Login(User user)
        {
            var loginPage = new SignInPage();
            loginPage.SetEmail(user.Email);
            loginPage.SetPassword(user.Password);
            loginPage.ClickSignIn();
        }
    }
}