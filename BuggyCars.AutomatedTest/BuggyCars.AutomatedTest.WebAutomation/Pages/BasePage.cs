using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using log4net;
using NUnit.Framework;
using BuggyCars.AutomatedTest.WebAutomation.Configuration;
using BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers;
using OpenQA.Selenium;

namespace BuggyCars.AutomatedTest.WebAutomation.Pages
{
    public abstract class BasePage
    {
        protected BasePage(
            AutomationSettings settings,
            BrowserBase browser,
            TestData testData)
        {
            Browser = browser;
            Settings = settings;
            TestData = testData;
        }

        [Flags]
        public enum ExecuteOptions
        {
            /// <summary>
            /// Do not check loading icon.
            /// </summary>
            None = 0,

            /// <summary>
            /// Check loading icon is not displayed.
            /// </summary>
            WaitForLoadToComplete = 1
        }

        public static ILog Logger { get; set; } = Log4NetHelper.GetLogger(typeof(Log4NetHelper));

        protected AutomationSettings Settings { get; }

        protected BrowserBase Browser { get; }

        protected TestData TestData { get; }

        public static void WaitLow()
        {
            Thread.Sleep(1000);
        }

        public void RefreshPage()
        {
            Browser.RefreshPage();
        }

        public void ClearLocalStorage()
        {
            var javascript = "javascript:localStorage.clear()";
            ExecuteJavaScript(javascript);
        }

        public void NavigateToUrlAndWaitUntilLoaded(string url, NamedLocator elementToVerifyPageIsLoaded)
        {
            Logger.Info("Opening Page - " + url);
            Browser.Navigate().GoToUrl(url);
            WaitUntilPageIsReady(elementToVerifyPageIsLoaded);
        }

        public IEnumerable<IWebElement> FindAllElements(NamedLocator locator, TimeSpan? timeoutSec = null)
        {
            Preconditions.NotNull(locator, nameof(locator));
            Logger.Info($"Finding all the elements: {locator.Description}");
            return Browser.FindAllElements(locator.Locator, timeoutSec ?? Timeouts.LoadingWait);
        }

        public bool IsEnabled(NamedLocator locator)
        {
            Preconditions.NotNull(locator, nameof(locator));

            Logger.Info($"Checking the element enabled: {locator.Description}");
            try
            {
                var element = GetWebElement(locator);
                return element.Enabled;
            }
            catch (WebDriverTimeoutException e)
            {
                Logger.Info(e.Message);
                return false;
            }
        }

        public void Click(NamedLocator locator)
        {
            Preconditions.NotNull(locator, nameof(locator));

            Logger.Debug($@"Clicking: {locator.Description} - Selector {locator.Locator}");
            GetWebElement(locator).Click();
            Logger.Debug($@"Element Clicked: {locator.Description} - Selector {locator.Locator}");
        }

        public void ClickAndWaitForElement(NamedLocator clickLocator, NamedLocator loadedLocator)
        {
            Preconditions.NotNull(loadedLocator, nameof(loadedLocator));
            Click(clickLocator);
            WaitForElement(loadedLocator);
        }

        public void ClickAndWaitForElementClickable(NamedLocator clickLocator, NamedLocator loadedLocator)
        {
            Preconditions.NotNull(loadedLocator, nameof(loadedLocator));
            Click(clickLocator);
            WaitForElementClickable(loadedLocator);
        }

        public void ClickAndWaitForElementNotDisplayed(NamedLocator clickLocator, NamedLocator loadedLocator)
        {
            Preconditions.NotNull(loadedLocator, nameof(loadedLocator));
            Click(clickLocator);
            WaitForElementNotDisplayed(loadedLocator);
        }

        public void ScrollToElement(NamedLocator locator)
        {
            Preconditions.NotNull(locator, nameof(locator));
            Logger.Info($"Scrolling to element: {locator.Description}");
            Browser.ScrollToElement(locator);
            Logger.Debug($"Element found: {locator.Description}");
        }

        public void MoveToElement(NamedLocator locator)
        {
            Preconditions.NotNull(locator, nameof(locator));
            Logger.Info($"Moving to element: {locator.Description}");
            Browser.MoveToElement(locator);
            Logger.Debug($"Element found: {locator.Description}");
        }

        public void ScrollToElementAndClick(NamedLocator locator)
        {
            ScrollToElement(locator);
            Click(locator);
        }

        public void MoveToElementAndClick(NamedLocator locator)
        {
            MoveToElement(locator);
            Click(locator);
        }

        public void MoveToElementClickAndWaitForElement(NamedLocator locator, NamedLocator loadedLocator)
        {
            ScrollToElement(locator);
            ClickAndWaitForElement(locator, loadedLocator);
        }

        public void ClearText(NamedLocator locator)
        {
            Preconditions.NotNull(locator, nameof(locator));

            Logger.Info($"Clearing text in: {locator.Description}");
            GetWebElement(locator).Clear();
        }

        public void EnterText(string inputText, NamedLocator locator)
        {
            Preconditions.NotNull(locator, nameof(locator));

            Logger.Info($"Entering text {inputText} in: {locator.Description}");
            GetWebElement(locator).SendKeys(inputText);
        }

        public void ClearAndEnterText(string inputText, NamedLocator locator)
        {
            Preconditions.NotNull(locator, nameof(locator));

            Logger.Info($"Clear and enter text {inputText} in: {locator.Description}");
            var element = GetWebElement(locator);

            // Note: clearing whole element in one call won't work when updating member details in member dashboard page.
            // Fix this issue by clearing character by character
            foreach (var value in element.GetAttribute("value"))
            {
                element.SendKeys(Keys.Backspace);
            }

            element.SendKeys(inputText);
        }

        public void SelectAllAndEnterText(string inputText, NamedLocator locator)
        {
            Preconditions.NotNull(locator, nameof(locator));

            Logger.Info($"Select all and enter text {inputText} in: {locator.Description}");
            var element = GetWebElement(locator);
            element.SendKeys(Keys.Control + "a");
            element.SendKeys(inputText);
        }

        public void PerformLastActionsAndFailTheTest(Exception e)
        {
            Preconditions.NotNull(e, nameof(e));
            var screenshotName = TestContext.CurrentContext.Test.Name;
            screenshotName = string.Concat(screenshotName.Split(Path.GetInvalidFileNameChars()));
            var date = DateTime.Now;
            var formattedDate = date.ToString("dd-MM_HH-mm", CultureInfo.InvariantCulture);
            var screenshotNameWithCurrentTime = screenshotName + "_" + formattedDate;
            var pageUrl = Browser.GetPageUrl();
            var screenshotPath = Browser.TakeScreenshot(screenshotNameWithCurrentTime);
            Logger.Error($"Test failed. Reason: {e.Message} .\n Screenshot: {screenshotPath} .\n Page url: {pageUrl}");
            Assert.Fail($"Test failed. Reason: {e.Message} .\n Screenshot: {screenshotPath} .\n Page url: {pageUrl}");
        }

        public IWebElement GetWebElement(NamedLocator locator)
        {
            return Browser.GetWebElement(locator);
        }

        public bool IsElementPresent(By locator)
        {
            return Browser.IsElementPresent(locator);
        }

        public IWebElement FindElement(By locator)
        {
            return Browser.FindElement(locator);
        }

        public void WaitForElement(NamedLocator locator)
        {
            Preconditions.NotNull(locator, nameof(locator));
            Browser.WaitUntilElementDisplayed(locator, timeout: Timeouts.LoadingWait);
        }

        public void WaitForElementClickable(NamedLocator locator)
        {
            Preconditions.NotNull(locator, nameof(locator));
            Browser.WaitUntilElementClickable(locator, timeout: Timeouts.LoadingWait);
        }

        public void WaitForElementNotDisplayed(NamedLocator locator)
        {
            Browser.WaitUntilElementNotDisplayed(locator);
        }

        public void TryWaitForElement(NamedLocator locator)
        {
            Browser.TryWaitUntilElementDisplayed(locator, timeout: Timeouts.LoadingTryWait);
        }

        public void WaitForElementThenNotDisplayed(NamedLocator locator)
        {
            var isElementDisplayed = Browser.TryWaitUntilElementDisplayed(locator, timeout: Timeouts.LoadingTryWait);
            if (isElementDisplayed)
            {
                WaitForElementNotDisplayed(locator);
            }
        }

        public void WaitUntilPageIsReady(NamedLocator elementToVerifyPageIsLoaded)
        {
            Preconditions.NotNull(elementToVerifyPageIsLoaded, nameof(elementToVerifyPageIsLoaded));
            WaitForElement(elementToVerifyPageIsLoaded);
        }

        public void SwitchToFrame(NamedLocator locator)
        {
            Preconditions.NotNull(locator, nameof(locator));
            Browser.SwitchToFrame(locator);
        }

        public void SwitchToDefaultContent()
        {
            Browser.SwitchToDefaultContent();
        }

        public void ClickOkOnPopup()
        {
            Browser.ClickOkOnPopup();
        }

        public void ClickCancelOnPopup()
        {
            Browser.ClickCancelOnPopup();
        }

        public string GetPopUpText()
        {
            return Browser.GetPopUpText();
        }

        public void ExecuteJavaScript(string javascript)
        {
            Logger.Info($"Executing javascript: {javascript}");
            Browser.ExecuteJavaScript(javascript);
        }

        public string AddTimeDate(string text)
        {
            var date = DateTime.Now;
            var formattedDate = date.ToString("MM_dd_HH_mm_ss", CultureInfo.InvariantCulture);
            text = $"{formattedDate}_{text}";
            return text;
        }

        public string GenerateRamdomEmail(string email)
        {
            Preconditions.NotNull(email, nameof(email));
            var date = DateTime.Now;
            var formattedDate = date.ToString("MM_dd_HH_mm_ss_fff", CultureInfo.InvariantCulture);
            var index = email.IndexOf("@", StringComparison.Ordinal);
            return $"{email.Substring(0, index)}_{formattedDate}{email[index..]}";
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "CA5394:Do not use insecure randomness", Justification = "Numbers not used in security-sensitive manner")]
        public int GenerateRamdomNumber()
        {
            Random random = new Random();
            return random.Next(000000000, 999999999);
        }

        public string GetMessage(StringMessages stringMessages, string key)
        {
            Preconditions.NotNull(stringMessages, nameof(stringMessages));

            if (!stringMessages.Strings.TryGetValue(key, out var message))
            {
                throw new NotFoundException($"Message is not defined for the key '{key}'");
            }

            return message;
        }

        public void SetValueIfNotNullOrEmpty(string value, Action<string> setter)
        {
            Preconditions.NotNull(setter, nameof(setter));
            if (!string.IsNullOrEmpty(value))
            {
                setter(value);
            }
        }

        internal void SwitchToParentFrame()
        {
            Browser.SwitchToParentFrame();
        }

        protected static void Repeat(int numberOfIterations, Action action)
        {
            Preconditions.NotNull(action, nameof(action));

            for (var step = 0; step < numberOfIterations; step++)
            {
                action();
            }
        }
    }
}
