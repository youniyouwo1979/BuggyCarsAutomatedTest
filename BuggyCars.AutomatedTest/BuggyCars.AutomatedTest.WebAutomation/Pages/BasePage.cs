using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using log4net;
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

        public static ILog Logger { get; set; } = Log4NetHelper.GetLogger(typeof(Log4NetHelper));

        protected AutomationSettings Settings { get; }

        protected BrowserBase Browser { get; }

        protected TestData TestData { get; }

        public static void WaitLow()
        {
            Thread.Sleep(1000);
        }

        public string GetText(NamedLocator locator)
        {
            Preconditions.NotNull(locator, nameof(locator));
            var element = GetWebElement(locator);
            return element.Text;
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

        public void Click(NamedLocator locator)
        {
            Preconditions.NotNull(locator, nameof(locator));

            Logger.Debug($@"Clicking: {locator.Description} - Selector {locator.Locator}");
            GetWebElement(locator).Click();
            Logger.Debug($@"Element Clicked: {locator.Description} - Selector {locator.Locator}");
        }

        public void ClearAndEnterText(string inputText, NamedLocator locator)
        {
            Preconditions.NotNull(locator, nameof(locator));

            Logger.Info($"Clear and enter text {inputText} in: {locator.Description}");
            var element = GetWebElement(locator);

            foreach (var value in element.GetAttribute("value"))
            {
                element.SendKeys(Keys.Backspace);
            }

            element.SendKeys(inputText);
        }

        public IWebElement GetWebElement(NamedLocator locator)
        {
            return Browser.GetWebElement(locator);
        }

        public void WaitForElement(NamedLocator locator)
        {
            Preconditions.NotNull(locator, nameof(locator));
            Browser.WaitUntilElementDisplayed(locator, timeout: Timeouts.LoadingWait);
        }

        public void WaitUntilPageIsReady(NamedLocator elementToVerifyPageIsLoaded)
        {
            Preconditions.NotNull(elementToVerifyPageIsLoaded, nameof(elementToVerifyPageIsLoaded));
            WaitForElement(elementToVerifyPageIsLoaded);
        }

        public void SetValueIfNotNullOrEmpty(string value, Action<string> setter)
        {
            Preconditions.NotNull(setter, nameof(setter));
            if (!string.IsNullOrEmpty(value))
            {
                setter(value);
            }
        }
    }
}
