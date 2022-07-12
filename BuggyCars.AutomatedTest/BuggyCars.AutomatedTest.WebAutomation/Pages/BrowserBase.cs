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

        public void UploadLocalFile(string path, By locator)
        {
            Preconditions.NotNull(locator, nameof(locator));
            var element = FindElement(locator);
            if (_driver is IAllowsFileDetection allowsDetection)
            {
                allowsDetection.FileDetector = new LocalFileDetector();
            }

            element.SendKeys(path);
        }

        public void DeleteUserSession()
        {
            _logger.Debug("Deleting cookies");
            _driver.Manage().Cookies.DeleteAllCookies();
        }

        public void BrowserMaximize()
        {
            _driver.Manage().Window.Maximize();
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
            var screenshotPath = string.Empty;

            if (_settings.IsOctopusDeployment)
            {
                var buildNameScreenshotUrl = _settings.RemoteExecutionBuildName.Replace('+', '_');
                screenshotPath = $"{_settings.VmLogHistoryPath}/{buildNameScreenshotUrl}/Logs/Screenshots/{screenshotName}.png";
            }
            else
            {
                screenshotPath = localScreenshotPath;
            }

            return screenshotPath;
        }

        public void GoBack()
        {
            _driver.Navigate().Back();
        }

        public void Forward()
        {
            _driver.Navigate().Forward();
        }

        public void RefreshPage()
        {
            _driver.Navigate().Refresh();
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

        public IWebElement FindElement(By locator)
        {
            IWebElement element = _driver.FindElement(locator);
            return element;
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

        public bool IsElementPresent(By locator)
        {
            try
            {
                _logger.Info("Checking for the element " + locator);
                return _driver.FindElements(locator).Count == 1;
            }
            catch (NoSuchElementException)
            {
                return false;
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

        public void WaitUntilElementClickable(NamedLocator locator, TimeSpan timeout)
        {
            Preconditions.NotNull(locator, nameof(locator));

            var wait = GetWebDriverWait(timeout);
            try
            {
                _logger.Debug($"Waiting for element to be clickable: {locator.Description}");
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator.Locator));
                _logger.Debug($"Element is clickable: {locator.Description}");
            }
            catch (Exception)
            {
                throw new NoSuchElementException($@"Element not clickable : {locator.Description} with locator: {locator.Locator}");
            }
        }

        public bool TryWaitUntilElementDisplayed(NamedLocator locator, TimeSpan timeout)
        {
            try
            {
                WaitUntilElementDisplayed(locator, timeout);
            }
            catch (NoSuchElementException)
            {
                return false;
            }

            return true;
        }

        public void WaitUntilElementNotDisplayed(NamedLocator locator)
        {
            Preconditions.NotNull(locator, nameof(locator));

            var wait = GetWebDriverWait(Timeouts.LoadingWait);
            try
            {
                _logger.Info($"Waiting for element to not be displayed: {locator.Description}");
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(locator.Locator));
            }
            catch (Exception)
            {
                throw new NoSuchElementException($@"Element should not be visible: {locator.Description} with locator: {locator.Locator}");
            }
        }

        public void SwitchToParentFrame()
        {
            _logger.Debug("Switching to parent frame ");
            _driver.SwitchTo().ParentFrame();
        }

        public WebDriverWait GetWebDriverWait(TimeSpan timeout)
        {
            var wait = new WebDriverWait(_driver, timeout);
            return wait;
        }

        public void SwitchToFrame(NamedLocator locator)
        {
            Preconditions.NotNull(locator, nameof(locator));

            var wait = GetWebDriverWait(Timeouts.LoadingWait);
            try
            {
                _logger.Debug($"Waiting for iframe to be availble: {locator.Description}");
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.FrameToBeAvailableAndSwitchToIt(locator.Locator));
                _logger.Debug($"switched to iframe: {locator.Description}");
            }
            catch (Exception)
            {
                throw new NoSuchElementException($"Frame not available : {locator.Description} with locator: {locator.Locator}");
            }
        }

        public void SwitchToDefaultContent()
        {
            _driver.SwitchTo().DefaultContent();
        }

        public bool IsPopupPresent()
        {
            try
            {
                _driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        public void ClickOkOnPopup()
        {
            if (!IsPopupPresent())
            {
                return;
            }

            _driver.SwitchTo().Alert().Accept();
        }

        public void ClickCancelOnPopup()
        {
            if (!IsPopupPresent())
            {
                return;
            }

            _driver.SwitchTo().Alert().Dismiss();
        }

        public string GetPopUpText()
        {
            if (!IsPopupPresent())
            {
                return string.Empty;
            }

            return _driver.SwitchTo().Alert().Text;
        }

        public string GetPageUrl() => _driver.Url.ToString();

        public void ExecuteJavaScript(string javascript)
        {
            _driver.ExecuteJavaScript(javascript);
        }

        /// <summary>
        /// Scrolls the top of element to the top of the visible area of the scrollable ancestor. Use for scrolling to elements in modals.
        /// </summary>
        /// <param name="locator">The element locator to scroll to.</param>
        public void ScrollToElement(NamedLocator locator)
        {
            Preconditions.NotNull(locator, nameof(locator));
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", GetWebElement(locator));
        }

        /// <summary>
        /// Moves to the centre of the element. Use for moving to elements on the page.
        /// </summary>
        /// <param name="locator">The element locator to move to.</param>
        public void MoveToElement(NamedLocator locator)
        {
            Preconditions.NotNull(locator, nameof(locator));
            Actions actions = new Actions(_driver);
            actions.MoveToElement(GetWebElement(locator));
            actions.Perform();
        }

        public void BrowserStackMarkSessionStatus()
        {
            if (_settings.BrowserHost == BrowserHost.BrowserStack)
            {
                if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed)
                {
                    _logger.Debug("Setting Browerstack Session status to passed");
                    ((IJavaScriptExecutor)_driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"passed\", \"reason\": \" Success \"}}");
                }
                else
                {
                    _logger.Debug("Setting Browerstack Session status to failed");
                    ((IJavaScriptExecutor)_driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \" Failed, see logs \"}}");
                }
            }
        }
    }
}
