using System;
using CSharpAutomationFramework.Framework.Core;
using OpenQA.Selenium;
using NUnit.Framework;
using System.Reflection;

namespace CSharpAutomationFramework.Tests.Base
{
    public class BaseTest
    {
        //Variables
        private String env;
        private String className;
        protected String browser;

        protected DriverFactory execDriver;
        protected Wrapper doAction;

        public IWebDriver driver = null;
        public Reporting Reporter;

        public void BeforeClass()
        {
            //get env
            env = System.Environment.GetEnvironmentVariable("envName");
            Assert.IsNotNull(env, "No environment Parameter value received");

            //Initiating execDriver
            execDriver = new DriverFactory();

            //Instantiate reporter
            Reporter = new Reporting(env, className, browser);
            Reporter.CreateSummaryReport();
        }

        public void BeforeMethod()
        {
            String testName = TestContext.CurrentContext.Test.MethodName;
            Console.WriteLine("Before Method for test " + testName);
            Reporter.CreateTestLevelReport(testName);
        }

        public void AfterMethod()
        {
            String testName = TestContext.CurrentContext.Test.MethodName;
            Console.WriteLine("After Method" + testName);
            Reporter.CloseTestLevelReport(testName);
        }

        public void AfterClass()
        {
            Console.WriteLine("After Class " + className);
            Reporter.CloseTestSummaryReport();
            if (driver != null)
                driver.Quit();
        }

        protected void SetClassName(Object obj)
        {
            className = obj.GetType().Name;
            Console.WriteLine("test class name: " +  className);
        }
    }
}
