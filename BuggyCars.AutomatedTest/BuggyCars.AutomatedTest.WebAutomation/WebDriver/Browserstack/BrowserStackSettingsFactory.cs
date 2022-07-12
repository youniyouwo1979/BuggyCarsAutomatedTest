using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using BuggyCars.AutomatedTest.WebAutomation.Configuration;
using BuggyCars.AutomatedTest.WebAutomation.WebDriver.Browserstack.Settings;
using OpenQA.Selenium;

namespace BuggyCars.AutomatedTest.WebAutomation.WebDriver.Browserstack
{
    public class BrowserStackSettingsFactory : IBrowserStackSettingsFactory
    {
        private static readonly Dictionary<string, Func<AutomationSettings, string, DriverOptions>> _map = new()
        {
            { BrowserName.ChromeDesktop.ToString(), BrowserStackDesktopSettings.GetChromeOptions },
            { BrowserName.FirefoxDesktop.ToString(), BrowserStackDesktopSettings.GetFirefoxOptions },
            { BrowserName.EdgeDesktop.ToString(), BrowserStackDesktopSettings.GetEdgeOptions },
            { BrowserName.SafariDesktop.ToString(), BrowserStackDesktopSettings.GetSafariOptions },
            { BrowserName.SamsungS21Chrome.ToString(), BrowserStackMobileSettings.GetSamsungS21Options },
            { BrowserName.Iphone12Safari.ToString(), BrowserStackMobileSettings.GetIphone12Options },
            { BrowserName.Iphone12ProSafari.ToString(), BrowserStackMobileSettings.GetIPhone12ProOptions }
        };

        private readonly AutomationSettings _settings;

        public BrowserStackSettingsFactory(
            AutomationSettings settings)
        {
            _settings = settings;
        }

        public DriverOptions GetBrowserStackDriverOptions(string browserName)
        {
            if (!_map.TryGetValue(browserName, out var browserSettings))
            {
                throw new ArgumentOutOfRangeException(nameof(browserName), $"Unsupported browser name: {browserName}.");
            }

            var fullTestName = TestContext.CurrentContext.Test.FullName;
            var sessionName = fullTestName.Split(new string[] { "FeatureFiles." }, StringSplitOptions.None).Last();

            return browserSettings.Invoke(_settings, sessionName);
        }
    }
}
