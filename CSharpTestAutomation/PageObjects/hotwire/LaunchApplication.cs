using System;
using CSharpAutomationFramework.Framework.Core;
using OpenQA.Selenium;

namespace CSharpAutomationFramework.PageObjects.hotwire
{
    public class LaunchApplication
    {
        private Reporting reporter;
        private IWebDriver driver;

        public LaunchApplication(IWebDriver driver, Reporting reporter)
        {
            this.reporter = reporter;
            this.driver = driver;
        }

        public SearchPage launchHotwire(String url)
        {
            driver.Navigate().GoToUrl(url);
            reporter.WriteToTestLevelReport("Navigate to specified URL", "URL: ", "Navigated to URL: ", "Done");
            return new SearchPage(driver, reporter);
        }
    }
}
