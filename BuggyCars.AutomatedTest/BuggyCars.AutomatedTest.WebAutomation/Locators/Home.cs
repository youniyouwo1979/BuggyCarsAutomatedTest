using BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers;
using OpenQA.Selenium;

namespace BuggyCars.AutomatedTest.WebAutomation.Locators
{
    public static partial class ElementLocators
    {
        internal static class Home
        {
            public static NamedLocator PopularMakeImage
                => NamedLocator.Create(By.XPath("//a[contains(@href, 'make')]"), "Popular Make image");

            public static NamedLocator PopularModelImage
                => NamedLocator.Create(By.XPath("//a[contains(@href, 'model')]"), "Popular Model image");

            public static NamedLocator OveralRatingImage
                => NamedLocator.Create(By.XPath("//a[contains(@href, 'overall')]"), "Overall Rating image");
        }
    }
}
