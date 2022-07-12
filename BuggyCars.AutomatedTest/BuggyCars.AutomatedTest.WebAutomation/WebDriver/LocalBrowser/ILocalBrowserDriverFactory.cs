namespace BuggyCars.AutomatedTest.WebAutomation.WebDriver.LocalBrowser
{
    public interface ILocalBrowserDriverFactory
    {
        LocalBrowserDriver GetLocalBrowserWebDriver(string browserName);
    }
}
