using System;
using CSharpAutomationFramework.Framework.Base;
using CSharpAutomationFramework.Framework.Core;
using CSharpAutomationFramework.Framework.Helpers;
using OpenQA.Selenium;

namespace CSharpAutomationFramework.PageObjects.Tcfa
{
    public class VehicleEntryPage : BasePage
    {
        //PageUiObjects
        //Customer App Data Objects
        readonly String edtCountOfOwnedVehicles = "id:=txtCountOfOwnedVehicles";
        readonly String edtNoOfCardsRequired = "id:=txtNoCardsRequired";
        readonly String lstCaptiveCard = "id:=ddlCaptiveCards";
        readonly String edtCaptiveCardsCount = "id:=txtCaptiveCardCount";
        readonly String radManualEntry = "id:=rbtManualEntry";
        readonly String edtVehicleEntry = "id:=txtVehicleEntry1";

        //buttons
        readonly String btnAdd = "id:=BtnAdd";
        readonly String btnSubmit = "id:=BtnSubmit";
        readonly String btnFinalSave = "id:=btnFinalSave";


        public VehicleEntryPage(IWebDriver driver, Reporting reporter) : base(driver, reporter) 
        {
            wrapper.SwitchToDefaultContent()
                   .SwitchToFrameWithName("main");
        }

        public VehicleEntryPage EnterCustomerVehicleInfo(String countOfVehicles, String noOfCards) 
        {
            wrapper.EnterText(edtCountOfOwnedVehicles, countOfVehicles)
                   .EnterText(edtNoOfCardsRequired, noOfCards);

            return this;
        }

        public VehicleEntryPage EnterVehicleDetailsManually() {
            String vehicleNo = Generic.RandomString(Generic.RandomStringType.Alpha, 2) + Generic.RandomString(Generic.RandomStringType.Numeric, 4);
            wrapper.Click(radManualEntry)
                   .EnterText(edtVehicleEntry, vehicleNo); //should start with alphabets end with number
            return this;
        }

        public VehicleEntryPage AddCaptiveCard(String captiveCard, String captiveCardCount)
        {
            wrapper.SelectOptionFromList(lstCaptiveCard, captiveCard)
                   .EnterText(edtCaptiveCardsCount, captiveCardCount)
                   .Click(btnAdd);

            return this;
        }

        public CustomerVehicleInformationPage SubmitVehicleEntryDetails()
        {
            wrapper.Click(btnSubmit)
                   .Click(btnFinalSave);
            return new CustomerVehicleInformationPage(driver, reporter);
        }
    }
}