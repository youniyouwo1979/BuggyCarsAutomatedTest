using BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers;
using OpenQA.Selenium;

namespace BuggyCars.AutomatedTest.WebAutomation.Locators
{
    public static partial class ElementLocators
    {
        internal static class GeneralElements
        {
            public static NamedLocator Text(string text)
                => NamedLocator.Create(By.XPath($"//div//*[contains(text(),'{text}')]"), $"Text - {text}");
        }
    }
}
