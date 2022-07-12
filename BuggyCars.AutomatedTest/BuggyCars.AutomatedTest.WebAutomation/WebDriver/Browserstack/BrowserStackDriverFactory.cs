using System;
using log4net;
using BuggyCars.AutomatedTest.WebAutomation.Configuration;
using BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers;
using BuggyCars.AutomatedTest.WebAutomation.WebDriver.Setup;
using OpenQA.Selenium.Remote;

namespace BuggyCars.AutomatedTest.WebAutomation.WebDriver.Browserstack
{
    public class BrowserStackDriverFactory : IBrowserStackDriverFactory
    {
        private const string BrowserStackURL = "https://hub-cloud.browserstack.com/wd/hub/";
        private static readonly ILog _logger = Log4NetHelper.GetLogger(typeof(Log4NetHelper));
        private readonly IBrowserStackSettingsFactory _browserStackSettingsFactory;
        private readonly BrowserStackService _browserStackFactory;
        private readonly AutomationSettings _settings;

        public BrowserStackDriverFactory(
            IBrowserStackSettingsFactory browserStackSettingsFactory, BrowserStackService browserStackFactory, AutomationSettings settings)
        {
            _settings = settings;
            _browserStackSettingsFactory = browserStackSettingsFactory;
            _browserStackFactory = browserStackFactory;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Logging exception")]
        public BrowserStackDriver GetBrowserStackWebDriver(string browserName)
        {
            var driverOptions = _browserStackSettingsFactory.GetBrowserStackDriverOptions(browserName);

            _browserStackFactory.CheckForAvailableBrowserStackSessions(60);
            if (_settings.BrowserHost == BrowserHost.BrowserStack)
            {
                _browserStackFactory.LaunchBrowserStackBinary();
            }

            try
            {
                _logger.Debug("Starting BrowserStack session");
                var webDriver = new RemoteWebDriver(new Uri(BrowserStackURL), driverOptions);

                return new BrowserStackDriver(webDriver, driverOptions);
            }
            catch (Exception e)
            {
                _logger.Error($"WebDriver failed:", e);
                throw;
            }
        }
    }
}
