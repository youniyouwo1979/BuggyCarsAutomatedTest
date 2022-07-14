using BuggyCars.AutomatedTest.WebAutomation.Configuration;
using BuggyCars.AutomatedTest.WebAutomation.Locators;

namespace BuggyCars.AutomatedTest.WebAutomation.Pages
{
    public class GeneralElements : BasePage
    {
        public GeneralElements(
            AutomationSettings settings,
            BrowserBase browser,
            TestData testData)
            : base(settings, browser, testData)
        {
        }

        public void VerifyTextDisplayed(string text)
        {
            WaitForElement(ElementLocators.GeneralElements.Text(text));
        }
    }
}
