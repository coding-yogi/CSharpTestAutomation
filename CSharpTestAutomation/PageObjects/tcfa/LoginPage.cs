using System;
using CSharpAutomationFramework.Framework.Base;
using CSharpAutomationFramework.Framework.Core;
using OpenQA.Selenium;

namespace CSharpAutomationFramework.PageObjects.Tcfa
{
    public class LoginPage : BasePage
    { 
        //PageUiObjects
        readonly String edtUserName = "id:=txtFLloginName";
        readonly String edtLocation = "id:=txtFLlocation";
        readonly String edtPassword = "id:=txtFLpassword";
        readonly String rdbtApplicationEntry = "id:=rdbtccnetAppEntry";
        readonly String btnLogin = "id:=imgbtnFLsubmit";
        readonly String lnkForgotPassword = "id:=btnResetPwd";

        public LoginPage(IWebDriver driver, Reporting reporter) : base(driver, reporter) {}


        public LoginPage EnterCredentials(String username, String location, String password)
        {
            wrapper.EnterText(edtUserName,username);
            wrapper.EnterText(edtLocation,location);
            wrapper.EnterText(edtPassword,password);
            wrapper.CheckCheckBox(rdbtApplicationEntry);
            reporter.WriteToTestLevelReport("Enter required details", "All fields should be filled", "All fields filled successfully", "Pass");
            return this;
        }

        public HomePage ClickLogin()
        {
            wrapper.Click(btnLogin);
            return new HomePage(driver, reporter);
        }

        public ForgotPasswordPage ClickForgotPassword()
        {
            wrapper.Click(lnkForgotPassword);
            return new ForgotPasswordPage(driver, reporter);
        }
        
    }
}
