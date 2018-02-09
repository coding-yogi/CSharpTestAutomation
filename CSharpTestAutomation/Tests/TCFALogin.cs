using System;
using CSharpAutomationFramework.Constants.Tcfa;
using CSharpAutomationFramework.Framework.Handlers;
using CSharpAutomationFramework.Tests;
using CSharpAutomationFramework.Tests.Base;
using NUnit.Framework;
using CSharpAutomationFramework.PageObjects.Tcfa;


namespace CSharpTestAutomation.Tests
{
    [TestFixtureSource(typeof(BrowserSource), "Browsers")]
    public class TCFALogin : BaseSeleniumWebTest
    {
        private readonly String configFile = "tcfa.json";
        private readonly String dataFile = "staticdata.json";
        DataHandler dHandler;

        public TCFALogin(String browser)
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
        public void TestValidateLogin()
        {
            LoginPage loginpage = new LaunchApplication(driver, Reporter)
               .launchTcfa(dHandler.GetAppConfig(TcfaConfigKeys.URL));

            loginpage.EnterCredentials(dHandler.GetAppData(TcfaDataKeys.USERNAME),
                                       dHandler.GetAppData(TcfaDataKeys.LOCATION),
                                       dHandler.GetAppData(TcfaDataKeys.PASSWORD))
                     .ClickLogin();
         }

        [Test]

        public void TestForgotPassword()
        {
            LoginPage loginpage = new LaunchApplication(driver, Reporter)
               .launchTcfa(dHandler.GetAppConfig(TcfaConfigKeys.URL));
   
            loginpage.ClickForgotPassword()
                     .EnterLoginIDAndRequestOTP(dHandler.GetAppData(TcfaDataKeys.LOGINID))
                     .EnterOTPAndValidate(dHandler.GetAppData(TcfaDataKeys.OTP))
                .EnterNewPassword("testpassword")
                .ClickSubmit();
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

