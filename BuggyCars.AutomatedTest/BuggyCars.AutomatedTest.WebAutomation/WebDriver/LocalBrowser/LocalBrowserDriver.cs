using OpenQA.Selenium;

namespace BuggyCars.AutomatedTest.WebAutomation.WebDriver.LocalBrowser
{
    public class LocalBrowserDriver
    {
        public LocalBrowserDriver(IWebDriver webDriver, DriverOptions driverOptions)
        {
            WebDriver = webDriver;
            DriverOptions = driverOptions;
        }

        public DriverOptions DriverOptions { get; }

        public IWebDriver WebDriver { get; }
    }
}
