using System;
using CSharpAutomationFramework.Framework.Base;
using CSharpAutomationFramework.Framework.Core;
using OpenQA.Selenium;

namespace CSharpAutomationFramework.PageObjects.Tcfa
{
    public class HomePage : BasePage
    {
        //PageUiObjects
        readonly String lnkMenu = "id:=lnkLHMenu";
        readonly String imgExpand = "xpath:=//a[text()='{0}']/../../td/a/img";
        readonly String rdSelectAll = "id:=rdfrgselect";
        readonly String rdRemoveAll = "id:=rdcrgremove";

        public HomePage(IWebDriver driver, Reporting reporter) : base(driver, reporter) {}
       
        public HomePage ExpandMenu(String menuName)
        {
            wrapper.SwitchToDefaultContent()
                   .SwitchToFrameWithName("header")
                   .Click(lnkMenu)
                   .SwitchToDefaultContent()
                   .SwitchToFrameWithName("contents")
                   .Click(String.Format(imgExpand, menuName));
            
            return this;
        }

        public HomePage SelectMenuItem(String menuItem)
        {
            wrapper.Click("linktext:=" + menuItem);
            return this;
        }

        public HomePage SelectAll()
        {
            wrapper.SwitchToDefaultContent()
                   .SwitchToFrameWithName("main")
                   .Click(rdSelectAll);
            
            return this;
        }

        public HomePage RemoveAll()
        {
            wrapper.SwitchToDefaultContent()
                   .SwitchToFrameWithName("main")
                   .Click(rdRemoveAll);
            
            return this;
        }
    }
}

