using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace BuggyCars.AutomatedTest.WebAutomation.WebDriver.Browserstack
{
    public class BrowserStackDriver
    {
        public BrowserStackDriver(
            RemoteWebDriver remoteWebDriver,
            DriverOptions driverOptions)
        {
            RemoteWebDriver = remoteWebDriver;
            DriverOptions = driverOptions;
        }

        public RemoteWebDriver RemoteWebDriver { get; }

        public DriverOptions DriverOptions { get; }
    }
}
