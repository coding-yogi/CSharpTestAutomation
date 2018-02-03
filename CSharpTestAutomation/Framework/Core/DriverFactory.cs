using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using CSharpAutomationFramework.Framework.Helpers;

namespace CSharpAutomationFramework.Framework.Core
{
    public class DriverFactory
    {
        private String webdriversPath;

        //Constructor
        public DriverFactory()
        {
            //Get Root Path
            String workingPath = Generic.GetBasePath();
            webdriversPath = workingPath + "/WebDrivers";
        }

        public IWebDriver GetWebDriver(String browser)
        {
            PlatformID osName = System.Environment.OSVersion.Platform;
            String webDriverType = browser.ToLower();

			switch (webDriverType)
			{
				case "firefox":
					return new FirefoxDriver(webdriversPath);
				case "chrome":
					return new ChromeDriver(webdriversPath);
				case "ie":
					return new InternetExplorerDriver(webdriversPath);
				default:
					return new ChromeDriver(webdriversPath);
			}
        }

        public IWebDriver GetRemoteWebDriver(String URL, ICapabilities dc) 
        {
            return new RemoteWebDriver(new Uri(URL),dc);
        }
    }
}
