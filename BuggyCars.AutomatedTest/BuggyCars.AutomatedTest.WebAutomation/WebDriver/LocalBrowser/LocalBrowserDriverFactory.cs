using System;
using System.Diagnostics;
using System.IO;
using BuggyCars.AutomatedTest.WebAutomation.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;

namespace BuggyCars.AutomatedTest.WebAutomation.WebDriver.LocalBrowser
{
    public class LocalBrowserDriverFactory : ILocalBrowserDriverFactory
    {
        private readonly ILocalBrowserSettingsFactory _localBrowserSettingsFactory;

        public LocalBrowserDriverFactory(
            ILocalBrowserSettingsFactory localBrowserSettingsFactory)
        {
            _localBrowserSettingsFactory = localBrowserSettingsFactory;
        }

        public LocalBrowserDriver GetLocalBrowserWebDriver(string browserName)
        {
            var driverOptions = _localBrowserSettingsFactory.GetLocalBrowserDriverOptions(browserName);

            if (!Enum.TryParse(typeof(BrowserName), browserName, out var browserNameType))
            {
                throw new ArgumentOutOfRangeException(nameof(browserName), "Unsupported Webdriver configuration.");
            }

            IWebDriver webDriver = browserNameType switch
            {
                BrowserName.ChromeDesktop => new ChromeDriver((ChromeOptions)driverOptions),
                BrowserName.FirefoxDesktop => new FirefoxDriver((FirefoxOptions)driverOptions),
                BrowserName.EdgeDesktop => new EdgeDriver((EdgeOptions)driverOptions),
                BrowserName.SafariDesktop => new SafariDriver((SafariOptions)driverOptions),
                _ => throw new ArgumentOutOfRangeException(nameof(browserName), "Unsupported Webdriver configuration.")
            };

            return new LocalBrowserDriver(webDriver, driverOptions);
        }
    }
}
