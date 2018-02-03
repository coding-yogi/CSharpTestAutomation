using System;
using System.Reflection;
using CSharpAutomationFramework.Framework.Core;

namespace CSharpAutomationFramework.Tests.Base
{
    public class BaseSeleniumWebTest : BaseTest
    {
        new public void AfterMethod()
        {
            base.AfterMethod();
            if (driver != null)
            {
                try
                {
                    driver.Quit();
                    driver = null;
                }
                catch (Exception) { }
            }
        }

        protected void SetWebDriver()
        {
            if(driver==null){
                driver = execDriver.GetWebDriver(browser);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                Reporter.SetDriver(driver);
                doAction = new Wrapper(driver, Reporter);
            }
        }
    }
}
