using System;
using CSharpAutomationFramework.Framework.Base;
using CSharpAutomationFramework.Framework.Core;
using OpenQA.Selenium;

namespace CSharpAutomationFramework.PageObjects.Tcfa
{
    public class ForgotPasswordPage : BasePage
    {
        //PageUiObjects
        readonly String edtLoginId = "id:=txtLoginID";
        readonly String edtOtp = "id:=txtOTP";
        readonly String edtNewPassword = "id:=txtNewPwd";
        readonly String edtReEnterNewPassword = "id:=txtRtypeNewPwd";
        readonly String btnRequestOtp = "id:=btnRequestOtp";
        readonly String btnValidate = "id:=btnValidate";
        readonly String btnSubmit = "id:=btnSubmit";
        readonly String btnReset = "id:=btnReset";
        readonly String btnCancel = "id:=btnCancel";

        public ForgotPasswordPage(IWebDriver driver, Reporting reporter) : base(driver, reporter) {}
 
        public ForgotPasswordPage EnterLoginIDAndRequestOTP(string loginId)
        {
            wrapper.EnterText(edtLoginId, loginId)
                   .Click(btnRequestOtp)
                   .AcceptAlert();
            return this;
        }
        public ForgotPasswordPage EnterOTPAndValidate(string otp)
        {
            wrapper.EnterText(edtOtp, otp)
                   .Click(btnValidate)
                   .AcceptAlert();
            return this;
        }
        public ForgotPasswordPage EnterNewPassword(string password)
        {
            wrapper.EnterText(edtNewPassword, password)
                   .EnterText(edtReEnterNewPassword, password);
            return this;
        }
        public LoginPage ClickSubmit()
        {
            wrapper.Click(btnSubmit)
                   .AcceptAlert();
            return new LoginPage(driver, reporter);
        }
    }
}
