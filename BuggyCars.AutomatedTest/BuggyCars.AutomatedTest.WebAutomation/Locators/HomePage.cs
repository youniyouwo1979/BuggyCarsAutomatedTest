using BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers;
using OpenQA.Selenium;

namespace BuggyCars.AutomatedTest.WebAutomation.Locators
{
    public static partial class ElementLocators
    {
        internal static class HomePage
        {
            public static NamedLocator PopularMakeImage
                => NamedLocator.Create(By.CssSelector("input[name='password']"), "Password textBox");
        }
    }
}
