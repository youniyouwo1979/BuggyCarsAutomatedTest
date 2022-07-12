using OpenQA.Selenium;

namespace BuggyCars.AutomatedTest.WebAutomation.WebDriver.Browserstack
{
    public interface IBrowserStackSettingsFactory
    {
        DriverOptions GetBrowserStackDriverOptions(string browserName);
    }
}
