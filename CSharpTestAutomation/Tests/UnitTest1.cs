using System;
using CSharpAutomationFramework.Constants.Hotwire;
using CSharpAutomationFramework.Framework.Handlers;
using CSharpAutomationFramework.Framework.Helpers;
using CSharpAutomationFramework.PageObjects.hotwire;
using CSharpAutomationFramework.Tests;
using CSharpAutomationFramework.Tests.Base;
using NUnit.Framework;

namespace CSharpAutomationFramework
{
    [TestFixtureSource(typeof(BrowserSource), "Browsers")]
    public class TestsForSearch : BaseSeleniumWebTest
    {
        private readonly String configFile = "hotwireconfig.json";
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

        [Test]
        public void TestMethod1()
        {
            String url = dHandler.GetAppConfig(HotwireConfigKeys.URL).ToString();

            SearchPage searchPage = new LaunchApplication(driver, Reporter)
                .launchHotwire(url);

            String fromCity = dHandler.GetAppData(HotwireDataKeys.FROM_CITY).ToString();
            String toCity = dHandler.GetAppData(HotwireDataKeys.TO_CITY).ToString();

            DateTime date = DateTime.Now;
            String departureDate = date.AddDays(1).ToString("MM\\/dd\\/yyy");
            String returnDate = date.AddDays(20).ToString("MM\\/dd\\/yyy");

            String packageName = "Flight + Hotel + Car";
            String startHour = "Evening";
            String endHour = "Morning";

            ResultsPage resultsPage = searchPage.selectPackage(packageName)
                .enterFlightDetails(fromCity, toCity, departureDate, startHour, returnDate, endHour)
                .findAVacation();


            int resultsCnt = resultsPage.WaitForLoader()
                .GetResultsCount();

            Assert.True(resultsCnt > 0);
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
