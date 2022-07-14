using System;
using log4net;
using BuggyCars.AutomatedTest.WebAutomation.Configuration;
using BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers;
//using BuggyCars.AutomatedTest.WebAutomation.WebDriver.Browserstack;
using BuggyCars.AutomatedTest.WebAutomation.WebDriver.LocalBrowser;
using OpenQA.Selenium;

namespace BuggyCars.AutomatedTest.WebAutomation.WebDriver
{
    public class WebDriverFactory : IWebDriverFactory
    {
        private static readonly ILog Logger = Log4NetHelper.GetLogger(typeof(WebDriverFactory));

        private readonly AutomationSettings _settings;
        //private readonly IBrowserStackDriverFactory _browserStackDriverFactory;
        private readonly ILocalBrowserDriverFactory _localBrowserDriverFactory;

        public WebDriverFactory(
            AutomationSettings settings,
            //IBrowserStackDriverFactory browserStackDriverFactory,
            ILocalBrowserDriverFactory localBrowserDriverFactory)
        {
            _settings = settings;
            //_browserStackDriverFactory = browserStackDriverFactory;
            _localBrowserDriverFactory = localBrowserDriverFactory;
        }

        public IWebDriver GetWebDriver(string browserName)
        {
            switch (_settings.BrowserHost)
            {
                case BrowserHost.Local:
                    Logger.Debug("Using Local Browser");
                    return _localBrowserDriverFactory.GetLocalBrowserWebDriver(browserName).WebDriver;

                //case BrowserHost.BrowserStack:
                //    Logger.Debug("Using BrowserStack");
                //    return _browserStackDriverFactory.GetBrowserStackWebDriver(browserName).RemoteWebDriver;

                default:
                    Logger.Error($"No Browser Configured with name: '{_settings.BrowserHost}'");
                    throw new NotSupportedException($"Unsupported browser '{_settings.BrowserHost}'");
            }
        }
    }
}
