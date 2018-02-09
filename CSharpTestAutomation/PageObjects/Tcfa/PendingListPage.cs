using System;
using CSharpAutomationFramework.Framework.Base;
using CSharpAutomationFramework.Framework.Core;
using CSharpAutomationFramework.PageObjects.Tcfa;
using OpenQA.Selenium;

namespace CSharpTestAutomation.PageObjects.Tcfa
{
    public class PendingListPage : BasePage
    {
        readonly String lnkPending = "linktext:=pending";

        public PendingListPage(IWebDriver driver, Reporting reporter) : base(driver, reporter) {
            wrapper.SwitchToDefaultContent()
                   .SwitchToFrameWithName("main");
        }

        public CustomerVehicleInformationPage SelectFirstPendingCustomer() {
            wrapper.Click(lnkPending);
            return new CustomerVehicleInformationPage(driver, reporter);
        }
    }
}
