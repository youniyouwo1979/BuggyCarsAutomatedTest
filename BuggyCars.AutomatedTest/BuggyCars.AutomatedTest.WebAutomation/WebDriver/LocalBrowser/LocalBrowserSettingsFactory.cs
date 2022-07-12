using System;
using System.Collections.Generic;
using BuggyCars.AutomatedTest.WebAutomation.Configuration;
using BuggyCars.AutomatedTest.WebAutomation.WebDriver.Settings;
using OpenQA.Selenium;

namespace BuggyCars.AutomatedTest.WebAutomation.WebDriver.LocalBrowser
{
    public class LocalBrowserSettingsFactory : ILocalBrowserSettingsFactory
    {
        private static readonly Dictionary<string, Func<AutomationSettings, DriverOptions>> _map = new()
        {
            { BrowserName.ChromeDesktop.ToString(), LocalBrowserSettings.GetChromeOptions },
            { BrowserName.FirefoxDesktop.ToString(), LocalBrowserSettings.GetFirefoxOptions },
            { BrowserName.EdgeDesktop.ToString(), LocalBrowserSettings.GetEdgeOptions },
            { BrowserName.SafariDesktop.ToString(), LocalBrowserSettings.GetSafariOptions }
        };

        private readonly AutomationSettings _settings;

        public LocalBrowserSettingsFactory(AutomationSettings settings)
        {
            _settings = settings;
        }

        public DriverOptions GetLocalBrowserDriverOptions(string browserName)
        {
            if (!_map.TryGetValue(browserName, out var browserSettings))
            {
                throw new ArgumentOutOfRangeException(nameof(browserName), "Unsupported browser configuration.");
            }

            return browserSettings.Invoke(_settings);
        }
    }
}
