using System;
using CSharpAutomationFramework.Framework.Base;
using CSharpAutomationFramework.Framework.Core;
using OpenQA.Selenium;

namespace CSharpAutomationFramework.PageObjects.Tcfa
{
    public class LaunchApplication : BasePage
    {
      
        public LaunchApplication(IWebDriver driver, Reporting reporter) : base(driver, reporter) {}

        public LoginPage launchTcfa(String url)
        {
            driver.Navigate().GoToUrl(url);
            reporter.WriteToTestLevelReport("Navigate to specified URL", "URL: ", "Navigated to URL: ", "Done");
            return new LoginPage(driver, reporter);
        }
    }
}
