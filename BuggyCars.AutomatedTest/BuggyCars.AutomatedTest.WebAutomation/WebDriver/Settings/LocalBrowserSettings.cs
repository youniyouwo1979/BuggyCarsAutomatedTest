using System;
using System.IO;
using BuggyCars.AutomatedTest.WebAutomation.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;

namespace BuggyCars.AutomatedTest.WebAutomation.WebDriver.Settings
{
    public static class LocalBrowserSettings
    {
        public static ChromeOptions GetChromeOptions(AutomationSettings settings)
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("start-maximized");
            chromeOptions.AddAdditionalOption(CapabilityType.AcceptSslCertificates, true);
            return chromeOptions;
        }

        public static FirefoxOptions GetFirefoxOptions(AutomationSettings settings)
        {
            var firefoxOptions = new FirefoxOptions();
            firefoxOptions.AddArgument("start-maximized");
            firefoxOptions.AddAdditionalOption(CapabilityType.AcceptSslCertificates, true);

            return firefoxOptions;
        }

        public static EdgeOptions GetEdgeOptions(AutomationSettings settings)
        {
            var edgeOptions = new EdgeOptions();
            edgeOptions.AddArgument("start-maximized");
            edgeOptions.AddAdditionalOption(CapabilityType.AcceptSslCertificates, true);
            return edgeOptions;
        }

        public static SafariOptions GetSafariOptions(AutomationSettings settings)
        {
            var safariOptions = new SafariOptions();
            safariOptions.AddAdditionalOption(CapabilityType.AcceptSslCertificates, true);
            return safariOptions;
        }
    }
}
