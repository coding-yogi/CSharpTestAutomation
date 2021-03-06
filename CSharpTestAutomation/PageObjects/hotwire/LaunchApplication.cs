﻿using System;
using CSharpAutomationFramework.Framework.Base;
using CSharpAutomationFramework.Framework.Core;
using OpenQA.Selenium;

namespace CSharpAutomationFramework.PageObjects.hotwire
{
    public class LaunchApplication : BasePage
    {
       
        public LaunchApplication(IWebDriver driver, Reporting reporter) : base(driver, reporter) {}

        public SearchPage launchHotwire(String url)
        {
            driver.Navigate().GoToUrl(url);
            reporter.WriteToTestLevelReport("Navigate to specified URL", "URL: ", "Navigated to URL: ", "Done");
            return new SearchPage(driver, reporter);
        }
    }
}
