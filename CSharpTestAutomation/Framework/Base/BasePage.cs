using System;
using CSharpAutomationFramework.Framework.Core;
using OpenQA.Selenium;

namespace CSharpAutomationFramework.Framework.Base
{
    public class BasePage
    {

        protected Reporting reporter;
        protected IWebDriver driver;
        protected Wrapper wrapper;

        protected BasePage(IWebDriver driver, Reporting reporter)
        {
            this.reporter = reporter;
            this.driver = driver;
            this.wrapper = new Wrapper(driver, reporter);
        }
    }
}