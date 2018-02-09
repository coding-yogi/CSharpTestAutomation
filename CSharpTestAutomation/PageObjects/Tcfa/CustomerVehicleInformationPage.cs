using System;
using CSharpAutomationFramework.Framework.Base;
using CSharpAutomationFramework.Framework.Core;
using CSharpAutomationFramework.Framework.Helpers;
using OpenQA.Selenium;

namespace CSharpAutomationFramework.PageObjects.Tcfa
{
    public class CustomerVehicleInformationPage : BasePage
    {
        //Customer Enrollment Objects
        readonly String edtDesignation = "id:=ddlEDBDesignation";
        readonly String edtName = "id:=txtEDBName";
        readonly String edtMobileNumber = "id:=txtEDBContactNo";

        //Dispatch of Card Details
        readonly String radTCC = "id:=RbtnDispatchCardTCC";
        readonly String btnSave = "id:=BtnSaveNExit";

        readonly String btnAttachments = "id:=BtnAttachments";
        readonly String btnVehicleEntry = "id:=btnVehicleEntry";

        public CustomerVehicleInformationPage(IWebDriver driver, Reporting reporter) : base(driver, reporter)
        {
            wrapper.SwitchToDefaultContent()
            .SwitchToFrameWithName("main");
        }

        public VehicleEntryPage ClickVehicleEntry() {
            wrapper.Click(btnVehicleEntry);
            return new VehicleEntryPage(driver, reporter);
        }

        public CustomerVehicleInformationPage EnterCustomerEnrollmentDetails(String designation) 
        {
            wrapper.SelectOptionFromList(edtDesignation, designation)
                   .EnterText(edtName, Generic.RandomString(Generic.RandomStringType.Alpha, 10))
                   .EnterText(edtMobileNumber, Generic.RandomString(Generic.RandomStringType.Numeric, 10));

            return this;
        }

        public AttachmentsPage ClickAttachments()
        {
            wrapper.Click(btnAttachments);
            return new AttachmentsPage(driver, reporter);
        }

        public CustomerVehicleInformationPage ClickSave()
        {
            wrapper.Click(btnSave)
                   .AcceptAlert();
            return this;
        }
    }
}
