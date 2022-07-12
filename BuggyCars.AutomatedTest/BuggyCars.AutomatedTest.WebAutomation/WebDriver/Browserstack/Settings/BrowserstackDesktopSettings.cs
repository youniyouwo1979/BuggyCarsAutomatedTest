using System.Collections.Generic;
using BuggyCars.AutomatedTest.WebAutomation.Configuration;
using BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;

namespace BuggyCars.AutomatedTest.WebAutomation.WebDriver.Browserstack.Settings
{
    public static class BrowserStackDesktopSettings
    {
        public static ChromeOptions GetChromeOptions(AutomationSettings settings, string sessionName)
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.BrowserVersion = "latest";
            var browserStackOptions = BrowserStackBaseSettings
                .GetSetDefaultDriverOptions(settings)
                .WithSeleniumVersion()
                .WithWindowsVersion()
                .WithSessionName(sessionName);

            chromeOptions.AddAdditionalOption("bstack:options", browserStackOptions);

            return chromeOptions;
        }

        public static FirefoxOptions GetFirefoxOptions(AutomationSettings settings, string sessionName)
        {
            var firefoxOptions = new FirefoxOptions();
            firefoxOptions.BrowserVersion = "latest";
            firefoxOptions.AddAdditionalOption("acceptSslCerts", true);
            var browserStackOptions = BrowserStackBaseSettings
                .GetSetDefaultDriverOptions(settings)
                .WithSeleniumVersion()
                .WithWindowsVersion()
                .WithSessionName(sessionName);

            firefoxOptions.AddAdditionalOption("bstack:options", browserStackOptions);

            return firefoxOptions;
        }

        public static EdgeOptions GetEdgeOptions(AutomationSettings settings, string sessionName)
        {
            var edgeOptions = new EdgeOptions();
            edgeOptions.BrowserVersion = "latest";
            var browserStackOptions = BrowserStackBaseSettings
                .GetSetDefaultDriverOptions(settings)
                .WithSeleniumVersion()
                .WithWindowsVersion()
                .WithSessionName(sessionName);

            edgeOptions.AddAdditionalOption("bstack:options", browserStackOptions);

            return edgeOptions;
        }

        public static SafariOptions GetSafariOptions(AutomationSettings settings, string sessionName)
        {
            Preconditions.NotNull(settings, nameof(settings));

            var safariOptions = new SafariOptions();
            safariOptions.BrowserVersion = "15.0";
            var browserStackOptions = BrowserStackBaseSettings
                .GetSetDefaultDriverOptions(settings)
                .WithSeleniumVersion()
                .WithSafariVersion()
                .WithSessionName(sessionName);

            browserStackOptions.Add("safari", GetSafariCapabilities());

            safariOptions.AddAdditionalOption("bstack:options", browserStackOptions);

            return safariOptions;
        }

        private static Dictionary<string, object> WithWindowsVersion(this Dictionary<string, object> driverOptions)
        {
            driverOptions.Add("os", "Windows");
            driverOptions.Add("osVersion", "10");
            return driverOptions;
        }

        private static Dictionary<string, object> WithSafariVersion(this Dictionary<string, object> driverOptions)
        {
            driverOptions.Add("os", "OS X");
            driverOptions.Add("osVersion", "Monterey");
            return driverOptions;
        }

        private static Dictionary<string, object> WithSeleniumVersion(this Dictionary<string, object> driverOptions)
        {
            driverOptions.Add("seleniumVersion", "4.1.0");
            return driverOptions;
        }

        private static Dictionary<string, object> GetSafariCapabilities()
        {
            Dictionary<string, object> safariCapabilities = new Dictionary<string, object>();
            safariCapabilities.Add("allowAllCookies", true);
            return safariCapabilities;
        }

        private static Dictionary<string, object> WithSessionName(this Dictionary<string, object> driverOptions, string sessionName)
        {
            driverOptions.Add("sessionName", sessionName);
            return driverOptions;
        }
    }
}
