using System;
using System.Threading;
using CSharpAutomationFramework.Framework.Base;
using CSharpAutomationFramework.Framework.Core;
using OpenQA.Selenium;

namespace CSharpAutomationFramework.PageObjects.hotwire
{
    public class ResultsPage : BasePage
    {
        private Reporting reporter;
        private IWebDriver driver;
        private Wrapper wrapper;

        //PageUiObjects
        private readonly String imgLoader = "cssselector:=span.loader";
        private readonly String lblResults = "cssselector:=p.showing-results";

        public ResultsPage(IWebDriver driver, Reporting reporter)
        {
            this.reporter = reporter;
            this.driver = driver;
            wrapper = new Wrapper(driver, reporter);
        }

        public ResultsPage WaitForLoader()
        {
            Boolean isLoaderVisible = wrapper.IsElementDisplayed(imgLoader);
            while (isLoaderVisible) {
                try {
                    isLoaderVisible =  wrapper.IsElementDisplayed(imgLoader);
                    Thread.Sleep(500);
                } catch (Exception e) {
                    Console.Write(e.StackTrace);
                }

            }

            return this;
        }

        public int GetResultsCount()
        {
            wrapper.Click(lblResults);
            String label = wrapper.GetElement(lblResults).Text;
            Char[] delimiter = { ' ' };
            int cnt = Convert.ToInt32(label.Split(delimiter)[6]);
            reporter.WriteToTestLevelReport("Get Results Count", "Results should be displayed", "Results count is " + cnt, "Pass");
            return cnt;

        }
    }
}
