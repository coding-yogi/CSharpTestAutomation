using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace CSharpAutomationFramework.Framework.Core
{
    public class Wrapper
    {
        private Reporting reporter;
        private IWebDriver driver;

        //Constructor
        public Wrapper(IWebDriver driver, Reporting reporter)
        {
            this.reporter = reporter;
            this.driver = driver;
        }

        public IWebElement GetElement(String objDesc)
        {
            //Delimiters
            String[] delimiters = { ":=" };
            String[] arrFindByValues = objDesc.Split(delimiters, StringSplitOptions.None);

            //Get Findby and Value
            String FindBy = arrFindByValues[0].ToLower();
            String val = arrFindByValues[1];

            //Handle all FindBy cases
            try 
            {
                switch (FindBy)
                {
                    case "id":
                        return driver.FindElement(By.Id(val));
                    case "name":
                        return driver.FindElement(By.Name(val));
                    case "linktext":
                        return driver.FindElement(By.LinkText(val));
                    case "classname":
                        return driver.FindElement(By.ClassName(val));
                    case "cssselector":
                        return driver.FindElement(By.CssSelector(val));
                    case "xpath":
                        return driver.FindElement(By.XPath(val));
                    case "tagname":
                        return driver.FindElement(By.TagName(val));
                    default:
                        reporter.WriteToTestLevelReport("Get element matching description " + objDesc, "Element should be found and returned", "Property " + FindBy + " specified for element is invalid", "Fail");
                        throw (new InvalidSelectorException("Wrapper method GetElement() : Property " + FindBy + " specified for element is invalid"));
                }
            }
            catch (NoSuchElementException ex)
            {
                reporter.WriteToTestLevelReport("Get element matching description " + objDesc, "Element should be found and returned", "Element not found", "Fail");
                throw (ex);
            }
        }

        public IReadOnlyList<IWebElement> GetElements(String objDesc)
        {
            //Delimiters
            String[] delimiters = { ":=" };
            String[] arrFindByValues = objDesc.Split(delimiters, StringSplitOptions.None);

            //Get Findby and Value
            String FindBy = arrFindByValues[0].ToLower();
            String val = arrFindByValues[1];

            //Handle all FindBy cases
            switch (FindBy)
            {
                case "id":
                    return driver.FindElements(By.Id(val));
                case "name":
                    return driver.FindElements(By.Name(val));
                case "linktext":
                    return driver.FindElements(By.LinkText(val));
                case "classname":
                    return driver.FindElements(By.ClassName(val));
                case "cssselector":
                    return driver.FindElements(By.CssSelector(val));
                case "xpath":
                    return driver.FindElements(By.XPath(val));
                case "tagname":
                    return driver.FindElements(By.TagName(val));
                default:
                    reporter.WriteToTestLevelReport("Get elements matching description " + objDesc, "Elements should be found and returned", "Property " + FindBy + " specified for element is invalid", "Fail");
                    throw (new InvalidSelectorException("Wrapper method GetElements() : Property " + FindBy + " specified for element is invalid"));
            }

        }

        public IWebElement GetChildElement(IWebElement parentElem, String objDesc)
        {
            //Delimiters
            String[] delimiters = { ":=" };
            String[] arrFindByValues = objDesc.Split(delimiters, StringSplitOptions.None);

            //Get Findby and Value
            String FindBy = arrFindByValues[0].ToLower();
            String val = arrFindByValues[1];

            //Handle all FindBy cases
            try
            {
                switch (FindBy)
                {
                    case "id":
                        return parentElem.FindElement(By.Id(val));
                    case "name":
                        return parentElem.FindElement(By.Name(val));
                    case "linktext":
                        return parentElem.FindElement(By.LinkText(val));
                    case "classname":
                        return parentElem.FindElement(By.ClassName(val));
                    case "cssselector":
                        return parentElem.FindElement(By.CssSelector(val));
                    case "xpath":
                        return parentElem.FindElement(By.XPath(val));
                    case "tagname":
                        return parentElem.FindElement(By.TagName(val));
                    default:
                        reporter.WriteToTestLevelReport("Get child object matching description " + objDesc, "Object should be found and returned", "Property " + FindBy + " specified for object is invalid", "Fail");
                        throw (new InvalidSelectorException("Wrapper method GetChildElement() : Property " + FindBy + " specified for element is invalid"));
                }
            }
            catch (NoSuchElementException ex)
            {
                reporter.WriteToTestLevelReport("Get element matching description " + objDesc, "Element should be found and returned", "Element not found", "Fail");
                throw (ex);
            }
        }

        public IReadOnlyList<IWebElement> GetChildElements(IWebElement parentElem, String objDesc)
        {
            //Delimiters
            String[] delimiters = { ":=" };
            String[] arrFindByValues = objDesc.Split(delimiters, StringSplitOptions.None);

            //Get Findby and Value
            String FindBy = arrFindByValues[0].ToLower();
            String val = arrFindByValues[1];

            //Handle all FindBy cases
            switch (FindBy)
            {
                case "id":
                    return parentElem.FindElements(By.Id(val));
                case "name":
                    return parentElem.FindElements(By.Name(val));
                case "linktext":
                    return parentElem.FindElements(By.LinkText(val));
                case "classname":
                    return parentElem.FindElements(By.ClassName(val));
                case "cssselector":
                    return parentElem.FindElements(By.CssSelector(val));
                case "xpath":
                    return parentElem.FindElements(By.XPath(val));
                case "tagname":
                    return parentElem.FindElements(By.TagName(val));
                default:
                    reporter.WriteToTestLevelReport("Get child elements matching description " + objDesc, "Child Elements should be found and returned", "Property " + FindBy + " specified for element is invalid", "Fail");
                    throw (new InvalidSelectorException("Wrapper method GetChildElements() : Property " + FindBy + " specified for element is invalid"));
            }
        }

        public Boolean IsElementPresent(String strDesc)
        {
            IReadOnlyList<IWebElement> lst = GetElements(strDesc);
            Boolean isPresent = (lst != null && lst.Count != 0);
            reporter.WriteToTestLevelReport("Element existence with description " + strDesc, "", "Element presence state is " + isPresent, "Done");
            return isPresent;
        }

        public Boolean IsChildElementPresent(IWebElement objParent, String strDesc)
        {
            IReadOnlyList<IWebElement> lst = GetChildElements(objParent, strDesc);
            Boolean isPresent = (lst != null && lst.Count != 0);
            reporter.WriteToTestLevelReport("Child Element existence with description " + strDesc, "", "Child Element presence state is " + isPresent, "Done");
            return isPresent;
        }

        public Boolean IsElementDisplayed(String strDesc) 
        {   
            IWebElement webElement = GetElement(strDesc);
            return IsElementDisplayed(webElement);
        }

        public Boolean IsElementDisplayed(IWebElement webElement)
        {
            Boolean bIsDisplayed = webElement.Displayed;
            String state = bIsDisplayed ? "displayed" : "not displayed";
            String strDesc = webElement.ToString();
            reporter.WriteToTestLevelReport("Check if object with description  " + strDesc + " is displayed", "", "Object is " + state, "Done");

            return  bIsDisplayed;
        }

        public Boolean IsElementEnabled(String strDesc)
        {
            //Get IWebElement
            IWebElement webElement = GetElement(strDesc);
            return IsElementEnabled(webElement);
        }

        public Boolean IsElementEnabled(IWebElement webElement)
        {
            //Check if the IWebElement is Enabled
            Boolean bIsEnabled = webElement.Enabled;
            String state = bIsEnabled ? "enabled" : "disabled";
            String strDesc = webElement.ToString();
            reporter.WriteToTestLevelReport("Check enabled state of object with description  " + strDesc, "", "Object state is " + state, "Done");

            return  bIsEnabled;
        }

        public Boolean IsElementSelected(String strDesc)
        {
            //Get IWebElement
            IWebElement webElement = GetElement(strDesc);
            return IsElementSelected(webElement);
        }

        public Boolean IsElementSelected(IWebElement webElement)
        {
            Boolean bIsSelected = webElement.Selected;
            String state = bIsSelected ? "selected" : "unselected";
            String strDesc = webElement.ToString();
            reporter.WriteToTestLevelReport("Check selected state of object with description  " + strDesc, "", "Object state is " + state, "Done");

            return bIsSelected;
        }

        public Wrapper Click(String strDesc)
        {
            //Initialize
            IWebElement webElement = GetElement(strDesc);
            return Click(webElement);
        }

        public Wrapper Click(IWebElement objClick)
        {
            String strDesc = objClick.ToString();

            //Check if the object is enabled, if yes click the same
            if (objClick.Displayed && objClick.Enabled)
                //Click on the object
                objClick.Click();
            else
            {
                reporter.WriteToTestLevelReport("Click element matching description " + strDesc, "Element with description " + strDesc + " should be clicked", "Element is either not displayed or is not enabled", "Fail");
                throw (new ElementNotVisibleException("Wrapper method click() : Element is either not visible or is not enabled"));
            }

            reporter.WriteToTestLevelReport("Click object matching description " + objClick.ToString(), "Click operation should be successful", "Successfully clicked the object", "Done");
            return this;
        }

        public Wrapper EnterText(String strDesc, String strText)
        {
            IWebElement webElement = GetElement(strDesc);
            return EnterText(webElement, strText);
        }

        public Wrapper EnterText(IWebElement objEdit, String strText)
        {
            String strDesc = objEdit.ToString();

            //Check if the object is enabled, if yes click the same
            if (objEdit.Displayed && objEdit.Enabled)
            {
                //Enter the text in the edit box
                objEdit.Clear();
                objEdit.SendKeys(strText);
            }
            else
            {
                reporter.WriteToTestLevelReport("Set value in element with description " + strDesc, "Value " + strText + " should be set in the edit box", "Element is either not displayed or not enabled", "Fail");
                throw (new ElementNotVisibleException("Wrapper method enterText() : Element with description " + strDesc + " is either not visible or is not enabled"));
                //return false;
            }

            reporter.WriteToTestLevelReport("Set value in element with description " + strDesc, "Value " + strText + " should be set in the edit box", "Value is set in the text field", "Done");
            return this;
        }

        public Wrapper SelectOptionFromList(String strDesc, String strText)
        {
            IWebElement webElement = GetElement(strDesc);
            return SelectOptionFromList(webElement, strText);
        }

        public Wrapper SelectOptionFromList(IWebElement objSelect, String strText)
        {
            String strDesc = objSelect.ToString();

            //Check if the object is enabled, if yes click the same
            if (objSelect.Displayed && objSelect.Enabled)
            {
                //Set Select Element and select required value by text
                try
                {
                    SelectElement select = new SelectElement(objSelect);
                    select.SelectByText(strText);
                }
                catch (WebDriverException ex)
                {
                    reporter.WriteToTestLevelReport("Select value in element with description " + strDesc, "Value " + strText + " should be selected in the list box", "Selecting value failed due to exception " + ex.Message, "Fail");
                    throw (ex);
                }
            }
            else
            {
                reporter.WriteToTestLevelReport("Select value in element with description " + strDesc, "Value " + strText + " should be selected in the list box", "Element is either not displayed or not enabled", "Fail");
                throw (new ElementNotVisibleException("Wrapper method selectOptionFromList() : Element with description " + strDesc + " is either not visible or is not enabled"));
                //return false;
            }

            reporter.WriteToTestLevelReport("Select value from dropdown", "Select value " + strText, "Value " + strText + " selected", "Done");
            return this;
        }

        public Wrapper CheckCheckBox(String strDesc)
        {
            //Initialize
            IWebElement webElement = GetElement(strDesc);
            return CheckCheckBox(webElement);
        }

        public Wrapper CheckCheckBox(IWebElement objChkBox)
        {
            String strDesc = objChkBox.ToString();

            //Check if the object is enabled, if yes click the same
            if (objChkBox.Displayed && objChkBox.Enabled)
            {
                //Check state of check box
                Boolean isChecked = objChkBox.Selected;

                //Check if Not Checked
                if (isChecked == false)
                    objChkBox.Click();
            }
            else
            {
                reporter.WriteToTestLevelReport("Check CheckBox element with description " + strDesc, "Checkbox should be checked", "Element is either not displayed or not enabled", "Fail");
                throw (new ElementNotVisibleException("Wrapper method checkCheckBox() : Element with description " + strDesc + " is either not visible or is not enabled"));
                //return false;
            }

            reporter.WriteToTestLevelReport("Check CheckBox element with description " + strDesc, "Check operation should be successful", "Successfully checked the checkbox", "Done");
            return this;
        }

        public Wrapper UncheckCheckBox(String strDesc)
        {
            //Initialize
            IWebElement webElement = GetElement(strDesc);
            return UncheckCheckBox(webElement);
        }

        public Wrapper UncheckCheckBox(IWebElement objChkBox)
        {
            String strDesc = objChkBox.ToString();

            //Check if the object is enabled, if yes click the same
            if (objChkBox.Displayed && objChkBox.Enabled)
            {
                //Check state of check box
                Boolean isChecked = objChkBox.Selected;

                //Check if Checked
                if (isChecked == true)
                    objChkBox.Click();
            }
            else
            {
                reporter.WriteToTestLevelReport("Check CheckBox element with description " + strDesc, "Checkbox should be unchecked", "Element is either not displayed or not enabled", "Fail");
                throw (new ElementNotVisibleException("Wrapper method checkCheckBox() : Element with description " + strDesc + " is either not visible or is not enabled"));
                //return false;
            }

            reporter.WriteToTestLevelReport("Uncheck CheckBox element with description " + strDesc, "Un-check operation should be successful", "Successfully unchecked the checkbox", "Done");
            return this;
        }

        public String GetCurrentBrowser()
        {
            try
            {
                ICapabilities DC = ((RemoteWebDriver)driver).Capabilities;
                return DC.BrowserName;
            }
            catch (WebDriverException e)
            {
                reporter.WriteToTestLevelReport("Get browser name", "Should return Browser Name", "Fetching Browser Name Failed. Exception " + e, "Fail");
                throw (e);
            }
        }

        public String getTitle()
        {
            return driver.Title;
        }

        public Wrapper maximizeWindow()
        {
            try
            {
                driver.Manage().Window.Maximize();
            }
            catch (WebDriverException e)
            {
                reporter.WriteToTestLevelReport("Maximize Window", "Window should be maximized successfully", "Error occured " + e.Message, "Fail");
                throw (e);
            }

            reporter.WriteToTestLevelReport("Maximize Window", "Window should be maximized successfully", "Window Maximized successfully", "Pass");
            return this;
        }

        public Wrapper SwitchToWindowWithName()
        {
            try {
                //driver.switchTo().window(strWindowName);
                //Switch to new window opened
                foreach(String handler in driver.WindowHandles)
                    driver.SwitchTo().Window(handler);
            }
            catch(Exception e) {
                reporter.WriteToTestLevelReport("Switch Window", "Switch to new Window ", "Exception occured : " + e.Message, "Fail");
                throw (e);
            }
            
            return this;
        }
    }
}