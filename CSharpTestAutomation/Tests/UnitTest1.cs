using System;

using CSharpAutomationFramework.Framework.Handlers;
using CSharpAutomationFramework.Framework.Helpers;

using CSharpAutomationFramework.Tests;
using CSharpAutomationFramework.Tests.Base;
using CSharpTestAutomation.PageObjects.hotwire;
using NUnit.Framework;

namespace CSharpAutomationFramework
{
    [TestFixtureSource(typeof(BrowserSource), "Browsers")]
    public class TestsForSearch : BaseSeleniumWebTest
    {
        private readonly String configFile = "hconfig.json";
        private readonly String dataFile = "staticdata.json";
        DataHandler dHandler;

        public TestsForSearch(String browser)
        {
            this.browser = browser;
            Environment.SetEnvironmentVariable("envName", TestContext.Parameters.Get("env"));
        }

        [OneTimeSetUp]
        new public void BeforeClass()
        {
            SetClassName(this);
            base.BeforeClass();

            dHandler = new DataHandler(configFile, dataFile);
        }

        [SetUp]
        new public void BeforeMethod()
        {
            SetWebDriver();
            base.BeforeMethod();
        }



        [TearDown]
        new public void AfterMethod()
        {
            base.AfterMethod();
        }

        [OneTimeTearDown]
        new public void AfterClass()
        {
            base.AfterClass();
        }
    }
}
