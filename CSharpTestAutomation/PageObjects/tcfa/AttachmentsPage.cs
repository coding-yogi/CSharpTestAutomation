using System;
using CSharpAutomationFramework.Framework.Base;
using CSharpAutomationFramework.Framework.Core;
using CSharpAutomationFramework.Framework.Helpers;
using OpenQA.Selenium;

namespace CSharpAutomationFramework.PageObjects.Tcfa
{
    public class AttachmentsPage : BasePage
    {
        readonly String btnSave = "id:=BtnSave";
        readonly String edtApplicationForm = "id:=BrowseAppForm";
        readonly String edtIdentityProof = "id:=BrowseIdentityProof";
        readonly String edtAddressProof = "id:=BrowseAddressProof";

        public AttachmentsPage(IWebDriver driver, Reporting reporter) : base(driver, reporter) { }

        public AttachmentsPage SwitchToAttachmentsWindow() {
            //code to switch to attachement window
            wrapper.SwitchToWindowWithName();
            return this;
        }

        public AttachmentsPage AttachDocs(String appForm, String identityProof, String addressProof) {

            String attachmentsFolder = Generic.GetBasePath() + "/Attachments/";

            //attach docs
            wrapper.EnterText(edtApplicationForm, attachmentsFolder + appForm)
                   .EnterText(edtIdentityProof, attachmentsFolder + identityProof)
                   .EnterText(edtAddressProof, attachmentsFolder + addressProof);
            return this;
        }

        public CustomerVehicleInformationPage SaveAttachments() {
            wrapper.Click(btnSave)
                   .AcceptAlert();
            return new CustomerVehicleInformationPage(driver, reporter);
        }
    }
}
