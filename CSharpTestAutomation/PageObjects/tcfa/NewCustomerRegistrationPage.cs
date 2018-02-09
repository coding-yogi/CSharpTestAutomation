using System;
using CSharpAutomationFramework.Framework.Base;
using CSharpAutomationFramework.Framework.Core;
using CSharpAutomationFramework.Framework.Helpers;
using OpenQA.Selenium;

namespace CSharpAutomationFramework.PageObjects.Tcfa
{
    public class NewCustomerRegistrationPage : BasePage
    {
        //PageUiObjects
        //Customer App Data Objects
        readonly String edtApplicationNo = "id:=txtApplicationNo";
        readonly String edtCarrierName = "id:=txtCarrierName";
        readonly String edtFirstName = "id:=txtAuthFirstName";
        readonly String edtMiddleName = "id:=txtAuthMiddleName";
        readonly String edtLastName = "id:=txtAuthLastName";
        //readonly String edtNameOnCard = "id:=txtNametoAppearonCard";
        readonly String btnCheck = "id:=BtnExistingApplication";

        //Address
        readonly String edtAddressLine1 = "id:=txtAddr1";
        readonly String edtAddressLine2 = "id:=txtAddr2";
        readonly String edtLandmark = "id:=txtAddrLandMark";
        readonly String lstState = "id:=ddlState";
        readonly String lstCity = "id:=ddlAddressCity";
        readonly String lstDistrict = "id:=ddlDistrict";
        readonly String edtPinCode = "id:=txtPinCode";

        //Contact
        readonly String edtSTDCode = "id:=txtSTD";
        readonly String edtTelephone1 = "id:=txtTelephone1";
        readonly String edtMobileNumber = "id:=txtMobileNo";

        //Route
        readonly String lstRouteType = "id:=ddlROfOperationSelect";
        readonly String lstRouteFrom = "id:=ddlROfOperationFrom";
        readonly String lstRouteTo = "id:=ddlROfOperationTo";

        //Account Information
        readonly String edtPanNo = "id:=txtPanNumber";
        readonly String lstTypeOfcard = "id:=ddlTypeOfCard";

        //Save
        readonly String btnSaveAndContinue = "id:=BtnSaveNcontinue";


        public NewCustomerRegistrationPage(IWebDriver driver, Reporting reporter) : base(driver, reporter)
        {
            wrapper.SwitchToDefaultContent()
                   .SwitchToFrameWithName("main");
        }

        public NewCustomerRegistrationPage ValidateApplicationNumber(String appNo)
        {
            wrapper.EnterText(edtApplicationNo, appNo)
                   .Click(btnCheck)
                   .AcceptAlert();
            return this;
        }

        public NewCustomerRegistrationPage EnterCarrierName(String carrier)
        {
            wrapper.EnterText(edtCarrierName, carrier);
            return this;
        }

        public NewCustomerRegistrationPage EnterCustomerName(String firstName, String middleName, String lastName)
        {
            wrapper.EnterText(edtFirstName, firstName)
                   .EnterText(edtMiddleName, middleName)
                   .EnterText(edtLastName, lastName);
            return this;
        }

        public NewCustomerRegistrationPage EnterAddressInfo(String state, String district, String city)
        {
            wrapper.EnterText(edtAddressLine1, Generic.RandomString(Generic.RandomStringType.AlphaNumeric,10))
                   .EnterText(edtAddressLine2, Generic.RandomString(Generic.RandomStringType.AlphaNumeric,10))
                   .EnterText(edtLandmark, Generic.RandomString(Generic.RandomStringType.AlphaNumeric,5))
                   .SelectOptionFromList(lstState, state)
                   .SelectOptionFromList(lstDistrict, district)
                   .SelectOptionFromList(lstCity, city)
                   .EnterText(edtPinCode, Generic.RandomString(Generic.RandomStringType.Numeric,6));

            return this;
        }

        public NewCustomerRegistrationPage EnterContactInformation(String stdCode, String telephoneNo, String MobileNo)
        {
            wrapper.EnterText(edtSTDCode, Generic.RandomString(Generic.RandomStringType.Numeric, 2))
                   .EnterText(edtTelephone1, Generic.RandomString(Generic.RandomStringType.Numeric, 10))
                   .EnterText(edtMobileNumber, Generic.RandomString(Generic.RandomStringType.Numeric, 10));

            return this;
        }

        public NewCustomerRegistrationPage EnterRouteOfOperation(String routeType, String routeFrom, String routeTo)
        {
            wrapper.SelectOptionFromList(lstRouteType, routeType)
                   .SelectOptionFromList(lstRouteFrom, routeFrom)
                   .SelectOptionFromList(lstRouteTo, routeTo);

            return this;
        }

        public NewCustomerRegistrationPage EnterAccountInformation(String panCard, String typeOfcard)
        {
            wrapper.EnterText(edtPanNo, panCard)
                   .SelectOptionFromList(lstTypeOfcard, typeOfcard);

            return this;
        }

        public CustomerVehicleInformationPage SubmitNewCustomerInformation()
        {
            wrapper.Click(btnSaveAndContinue)
                   .AcceptAlert();
            return new CustomerVehicleInformationPage(driver, reporter);
        }
    }
}
