using OpenQA.Selenium;

namespace BuggyCars.AutomatedTest.WebAutomation.WebDriver
{
    public interface IWebDriverFactory
    {
        IWebDriver GetWebDriver(string browserName);
    }
}
