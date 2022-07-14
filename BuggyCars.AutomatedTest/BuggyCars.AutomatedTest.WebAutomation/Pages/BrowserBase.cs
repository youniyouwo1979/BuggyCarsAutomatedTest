using System;
using System.Collections.ObjectModel;
using System.IO;
using log4net;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using BuggyCars.AutomatedTest.WebAutomation.Configuration;
using BuggyCars.AutomatedTest.WebAutomation.WebDriver.Settings;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers;
using BuggyCars.AutomatedTest.WebAutomation.WebDriver;

namespace BuggyCars.AutomatedTest.WebAutomation.Pages
{
    public sealed class BrowserBase : IDisposable
    {
        private static readonly ILog _logger = Log4NetHelper.GetLogger(typeof(Log4NetHelper));
        private readonly IWebDriverFactory _webDriverFactory;
        private readonly AutomationSettings _settings;
        private IWebDriver _driver;

        public BrowserBase(
           IWebDriverFactory webDriverFactory, AutomationSettings automationSettings)
        {
            _webDriverFactory = webDriverFactory;
            _settings = automationSettings;
        }

        public static void CreateScreenshotDirectory()
        {
            var screenshotDirectory = $"Logs\\Screenshots";

            if (Directory.Exists(screenshotDirectory))
            {
                Directory.Delete(screenshotDirectory, true);
            }

            Directory.CreateDirectory(screenshotDirectory);
        }

        public void SetUp()
        {
            var browser = _settings.Browser.ToString();
            _logger.Debug($"Browser name: {browser}");
            _driver = _webDriverFactory.GetWebDriver(browser);
            _logger.Debug("Deleting user session");
            DeleteUserSession();

            if (Enum.TryParse<DesktopBrowsers>(browser, true, out var desktopBrowsers))
            {
                _driver.Manage().Window.Maximize();
            }
        }

        public void Dispose()
        {
            _logger.Debug("Terminating driver session");
            if (_settings.BrowserHost == BrowserHost.BrowserStack)
            {
                // Quit is used by BrowserStack to identify that a test has completed. Quit calls the driver.Dispose method.
                _driver?.Quit();
            }
            else
            {
                _driver?.Dispose();
            }
        }

        public void DeleteUserSession()
        {
            _logger.Debug("Deleting cookies");
            _driver.Manage().Cookies.DeleteAllCookies();
        }

        public INavigation Navigate()
        {
            return _driver.Navigate();
        }

        public string TakeScreenshot(string screenshotName)
        {
            var ss = ((ITakesScreenshot)_driver).GetScreenshot();
            var filePath = GetType().Assembly.Location;
            var folderPath = Path.GetDirectoryName(filePath);
            var localScreenshotPath = $"{folderPath}\\Logs\\Screenshots\\{screenshotName}.png";
            ss.SaveAsFile(localScreenshotPath, ScreenshotImageFormat.Png);

            return localScreenshotPath;
        }

        public IWebElement GetWebElement(NamedLocator locator)
        {
            Preconditions.NotNull(locator, nameof(locator));
            try
            {
                _logger.Debug($@"Finding the for the element: {locator.Description} Locator: {locator.Locator}");
                var wait = new WebDriverWait(_driver, Timeouts.LoadingWait);
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator.Locator));
            }
            catch (NoSuchElementException)
            {
                _logger.Error("No element found " + locator.Description);
                throw;
            }
        }

        public ReadOnlyCollection<IWebElement> FindAllElements(By locator, TimeSpan timeoutSec)
        {
            var wait = new WebDriverWait(_driver, timeoutSec);
            try
            {
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
            }
            catch (WebDriverTimeoutException)
            {
                _logger.Error($"Error: Unable to find elements with By reference '{locator}'");
                Console.WriteLine($"Unable to find elements with By reference '{locator}'");
                throw;
            }
        }

        public void WaitUntilElementDisplayed(NamedLocator locator, TimeSpan timeout)
        {
            Preconditions.NotNull(locator, nameof(locator));

            var wait = GetWebDriverWait(timeout);
            try
            {
                _logger.Debug($"Waiting for element: {locator.Description}");
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator.Locator));
                _logger.Debug($"Element is displayed: {locator.Description}");
            }
            catch (Exception)
            {
                throw new NoSuchElementException($@"Element not visible : {locator.Description} with locator: {locator.Locator}");
            }
        }

        public WebDriverWait GetWebDriverWait(TimeSpan timeout)
        {
            var wait = new WebDriverWait(_driver, timeout);
            return wait;
        }

        public string GetPageUrl() => _driver.Url.ToString();
    }
}
