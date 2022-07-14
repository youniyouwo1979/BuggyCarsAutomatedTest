using BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers;
using OpenQA.Selenium;

namespace BuggyCars.AutomatedTest.WebAutomation.Locators
{
    public static partial class ElementLocators
    {
        internal static class UpdateMemberProfile
        {
            public static NamedLocator FirstName
                => NamedLocator.Create(By.Id("firstName"), "First name textBox");

            public static NamedLocator LastName
                => NamedLocator.Create(By.Id("lastName"), "Last name textBox");

            public static NamedLocator Phone
                => NamedLocator.Create(By.Id("phone"), "Phone textBox");

            public static NamedLocator SaveButton
                => NamedLocator.Create(By.XPath("//div[@role='main']//button[@type='submit']"), "Save button");

            public static NamedLocator Gender(string gender)
                => NamedLocator.Create(By.XPath($"//*[contains(text(),'{gender}')]"), $"Gender - {gender}");
        }
    }
}
