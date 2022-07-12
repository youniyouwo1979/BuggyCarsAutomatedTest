using OpenQA.Selenium;

namespace BuggyCars.AutomatedTest.WebAutomation.WebDriver.LocalBrowser
{
    public interface ILocalBrowserSettingsFactory
    {
        DriverOptions GetLocalBrowserDriverOptions(string browserName);
    }
}
