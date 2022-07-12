namespace BuggyCars.AutomatedTest.WebAutomation.WebDriver.Browserstack
{
    public interface IBrowserStackDriverFactory
    {
        BrowserStackDriver GetBrowserStackWebDriver(string browserName);
    }
}
