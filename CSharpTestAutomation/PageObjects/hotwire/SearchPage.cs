using System;
using CSharpAutomationFramework.Framework.Base;
using CSharpAutomationFramework.Framework.Core;
using OpenQA.Selenium;

namespace CSharpAutomationFramework.PageObjects.hotwire
{
    public class SearchPage : BasePage
    {
        private Reporting reporter;
        private IWebDriver driver;
        private Wrapper wrapper;

        //PageUiObjects
        private readonly String lnkVacation = "linktext:=Vacations";
        private readonly String edtFlyFrom = "id:=origin";
        private readonly String edtFlyTo = "name:=destination";
        private readonly String edtStartDate = "cssselector:=input.startDate";
        private readonly String edtEndDate = "cssselector:=input.endDate";
        private readonly String lstStartHour = "id:=startHour";
        private readonly String lstEndHour = "id:=endHour";
        private readonly String btnFindAVacation = "xpath:=//button/span[text()='Find a vacation']";


        public SearchPage(IWebDriver driver, Reporting reporter)
        {
            this.reporter = reporter;
            this.driver = driver;
            wrapper = new Wrapper(driver, reporter);
        }


        public SearchPage selectPackage(String packageName)
        {
            wrapper.Click(lnkVacation);
            wrapper.Click("xpath:=//label[text()='" + packageName + "']");
            return this;
        }

        public SearchPage enterFlightDetails(String fromCityCode, String toCityCode, String startDate, String startHour, String endDate, String endHour)
        {
            String xpathForCity = "xpath:=//a/strong[text()='{0}']";

            wrapper.EnterText(edtFlyFrom, fromCityCode)
                .Click(String.Format(xpathForCity, fromCityCode))
                .EnterText(edtFlyTo, toCityCode)
                .Click(String.Format(xpathForCity, toCityCode))
                .EnterText(edtStartDate, startDate)
                .SelectOptionFromList(lstStartHour, startHour)
                .EnterText(edtEndDate, endDate)
                .SelectOptionFromList(lstEndHour, endHour);

            reporter.WriteToTestLevelReport("Enter required details", "All fields should be filled", "All fields filled successfully", "Pass");
            return this;
        }

        public ResultsPage findAVacation()
        {
            wrapper.Click(btnFindAVacation);
            return new ResultsPage(driver, reporter);
        }

    }
}
