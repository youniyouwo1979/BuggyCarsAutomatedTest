using System.Collections.Generic;
using BuggyCars.AutomatedTest.WebAutomation.Configuration;
using BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Safari;

namespace BuggyCars.AutomatedTest.WebAutomation.WebDriver.Browserstack.Settings
{
    public static class BrowserStackMobileSettings
    {
        public static SafariOptions GetIphone12Options(AutomationSettings settings, string sessionName)
        {
            var iphoneOptions = new SafariOptions();

            var browserStackOptions = BrowserStackBaseSettings
            .GetSetDefaultDriverOptions(settings)
            .WithRealMobile()
            .WithDeviceVersion(deviceName: "iPhone 12", operatingSystem: "14")
            .WithSessionName(sessionName);

            iphoneOptions.AddAdditionalOption("bstack:options", browserStackOptions);

            return iphoneOptions;
        }

        public static SafariOptions GetIPhone12ProOptions(AutomationSettings settings, string sessionName)
        {
            var iphoneOptions = new SafariOptions();

            var browserStackOptions = BrowserStackBaseSettings
            .GetSetDefaultDriverOptions(settings)
            .WithRealMobile()
            .WithDeviceVersion(deviceName: "iPhone 12 Pro", operatingSystem: "14")
            .WithSessionName(sessionName);

            iphoneOptions.AddAdditionalOption("bstack:options", browserStackOptions);

            return iphoneOptions;
        }

        public static ChromeOptions GetSamsungS21Options(AutomationSettings settings, string sessionName)
        {
            var samsungOptions = new ChromeOptions();
            var browserStackOptions = BrowserStackBaseSettings
            .GetSetDefaultDriverOptions(settings)
            .WithRealMobile()
            .WithDeviceVersion(deviceName: "Samsung Galaxy S21", operatingSystem: "11.0")
            .WithSessionName(sessionName);

            samsungOptions.AddAdditionalOption("bstack:options", browserStackOptions);

            return samsungOptions;
        }

        private static Dictionary<string, object> WithRealMobile(this Dictionary<string, object> driverOptions)
        {
            driverOptions.Add("realMobile", true);
            return driverOptions;
        }

        private static Dictionary<string, object> WithDeviceVersion(this Dictionary<string, object> driverOptions, string deviceName, string operatingSystem)
        {
            driverOptions.Add("deviceName", deviceName);
            driverOptions.Add("osVersion", operatingSystem);
            return driverOptions;
        }

        private static Dictionary<string, object> WithSessionName(this Dictionary<string, object> driverOptions, string sessionName)
        {
            driverOptions.Add("sessionName", sessionName);
            return driverOptions;
        }
    }
}
