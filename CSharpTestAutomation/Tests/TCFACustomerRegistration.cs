using System;
using CSharpAutomationFramework.Constants.Tcfa;
using CSharpAutomationFramework.Framework.Handlers;
using CSharpAutomationFramework.PageObjects.Tcfa;
using CSharpAutomationFramework.Tests;
using CSharpAutomationFramework.Tests.Base;
using static CSharpAutomationFramework.Framework.Helpers.Generic;
using NUnit.Framework;
using CSharpTestAutomation.PageObjects.Tcfa;

namespace CSharpTestAutomation.Tests
{

    [TestFixtureSource(typeof(BrowserSource), "Browsers")]
    public class TCFACustomerRegistration : BaseSeleniumWebTest 
    {
       
        private readonly String configFile = "tcfa.json";
        private readonly String dataFile = "staticdata.json";
        DataHandler dHandler;

        public TCFACustomerRegistration(String browser)
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

            LoginPage loginpage = new LaunchApplication(driver, Reporter)
                .launchTcfa(dHandler.GetAppConfig(TcfaConfigKeys.URL));

            loginpage.EnterCredentials(dHandler.GetAppData(TcfaDataKeys.USERNAME),
                                       dHandler.GetAppData(TcfaDataKeys.LOCATION),
                                       dHandler.GetAppData(TcfaDataKeys.PASSWORD))
                     .ClickLogin();
        }

        [Test]
        public void TestNewCustomerRegistration()
        {
            HomePage homePage = new HomePage(driver, Reporter);
            homePage.ExpandMenu("Registration")
                    .SelectMenuItem("New Customer Registration");

            NewCustomerRegistrationPage newCustomerRegistrationPage = new NewCustomerRegistrationPage(driver, Reporter);
            newCustomerRegistrationPage.ValidateApplicationNumber(RandomString(RandomStringType.Numeric, 10))
                                       .EnterCarrierName(RandomString(RandomStringType.Alpha, 10))
                                       .EnterCustomerName(RandomString(RandomStringType.Alpha, 10),
                                                          RandomString(RandomStringType.Alpha, 10),
                                                          RandomString(RandomStringType.Alpha, 10))
                                       .EnterAddressInfo(dHandler.GetAppData(TcfaDataKeys.STATE),
                                                         dHandler.GetAppData(TcfaDataKeys.DISTRICT),
                                                         dHandler.GetAppData(TcfaDataKeys.CITY))
                                       .EnterContactInformation(RandomString(RandomStringType.Numeric, 2),
                                                                RandomString(RandomStringType.Numeric, 10),
                                                                RandomString(RandomStringType.Numeric, 10))
                                       .EnterRouteOfOperation(dHandler.GetAppData(TcfaDataKeys.ROUTE_TYPE),
                                                              dHandler.GetAppData(TcfaDataKeys.ROUTE_FROM),
                                                              dHandler.GetAppData(TcfaDataKeys.ROUTE_TO))
                                       .EnterAccountInformation(RandomString(RandomStringType.Alpha, 5) +
                                                                RandomString(RandomStringType.Numeric, 4) +
                                                                RandomString(RandomStringType.Alpha, 1),
                                                                dHandler.GetAppData(TcfaDataKeys.TYPE_OF_CARD))
                                       .SubmitNewCustomerInformation()
                                       .EnterCustomerEnrollmentDetails(dHandler.GetAppData(TcfaDataKeys.DESIGNATION))
                                       .ClickVehicleEntry()
                                       .EnterCustomerVehicleInfo("1", "2")
                                       .EnterVehicleDetailsManually()
                                       .AddCaptiveCard(dHandler.GetAppData(TcfaDataKeys.CAPTIVE_CARD), "1")
                                       .SubmitVehicleEntryDetails()
                                       .ClickAttachments()
                                       .SwitchToAttachmentsWindow()
                                       .AttachDocs(dHandler.GetAppData(TcfaDataKeys.APP_FORM_PATH),
                                                   dHandler.GetAppData(TcfaDataKeys.IDENTITY_PROOF_PATH),
                                                   dHandler.GetAppData(TcfaDataKeys.ADD_PROOF_PATH))
                                       .SaveAttachments()
                                       .ClickSave();

        }

        [Test]
        public void TestPendingCustomerRegistration()
        {
            HomePage homePage = new HomePage(driver, Reporter);
            homePage.ExpandMenu("Registration")
                    .SelectMenuItem("Pending Customer Registration");

            PendingListPage pendingListPage = new PendingListPage(driver, Reporter);
            pendingListPage.SelectFirstPendingCustomer()
                           .EnterCustomerEnrollmentDetails(dHandler.GetAppData(TcfaDataKeys.DESIGNATION))
                           .ClickVehicleEntry()
                           .EnterCustomerVehicleInfo("1", "2")
                           .EnterVehicleDetailsManually()
                           .AddCaptiveCard(dHandler.GetAppData(TcfaDataKeys.CAPTIVE_CARD), "1")
                           .SubmitVehicleEntryDetails()
                           .ClickAttachments()
                           .SwitchToAttachmentsWindow()
                           .AttachDocs(dHandler.GetAppData(TcfaDataKeys.APP_FORM_PATH),
                                       dHandler.GetAppData(TcfaDataKeys.IDENTITY_PROOF_PATH),
                                       dHandler.GetAppData(TcfaDataKeys.ADD_PROOF_PATH))
                           .SaveAttachments()
                           .ClickSave();
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
