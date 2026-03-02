using NUnit.Framework;
using UI.Interactions.Pages;

namespace UI.Tests
{
    public class DemoUiTests : BaseUiTest
    {
        [SetUp]
        public void BeforeTest()
        {
        }

        [TearDown]
        public void AfterTest()
        {
        }

        [Test]
        public void SignInTest()
        {
            var signInPage = new SignInPage();
            signInPage.SetEmail("ttt@ttt.com");
            signInPage.SetPassword("123456");
            signInPage.ClickSignIn();
            new AccountPage().AssertOpened();
        }
    }
}