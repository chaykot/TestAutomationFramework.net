using System;
using System.IO;
using Infrastructure.Configuration;
using Infrastructure.Logger;
using Infrastructure.Utils;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using UI.Interactions.BrowserDriver;

namespace UI.Tests
{
    public class BaseUiTest
    {
        protected static Browser Browser => Browser.Instance;

        [OneTimeSetUp]
        public void BaseOneTimeSetUp()
        {
        }

        [SetUp]
        public void BaseSetUp()
        {
            Browser.MaximizeWindow();
            Browser.OpenStartUrl(Configuration.Browser.StartUrl);
        }

        [TearDown]
        public void BaseTearDown()
        {
            if (Configuration.MakeScreenshotOnFail)
            {
                MakeScreenshotOnFail();
            }
            Browser.Close();
        }

        [OneTimeTearDown]
        public void BaseOneTimeTearDown()
        {
        }

        private static void MakeScreenshotOnFail()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                try
                {
                    Screenshot screenshot = ((ITakesScreenshot)Browser.Driver).GetScreenshot();
                    var screenshotPath = Path.Combine(Directory.GetCurrentDirectory(),
                        $"screenshot_{TestNameUtil.TestName}_{DateTime.Now:MM-dd-yyyy-HH-mm-ss}.png");
                    screenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
                    TestContext.AddTestAttachment(screenshotPath);
                }
                catch (Exception e)
                {
                    LogHelper.Instance.Warning($"Cannot make screenshot on fail: {e}");
                }
            }
        }
    }
}