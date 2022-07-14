using BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers;
using OpenQA.Selenium;

namespace BuggyCars.AutomatedTest.WebAutomation.Locators
{
    public static partial class ElementLocators
    {
        internal static class SignUp
        {
            public static NamedLocator FirstName
                => NamedLocator.Create(By.Id("firstName"), "First name textBox");

            public static NamedLocator LastName
                => NamedLocator.Create(By.Id("lastName"), "Last name textBox");

            public static NamedLocator Login
                => NamedLocator.Create(By.Id("username"), "Login textBox");

            public static NamedLocator Password
                => NamedLocator.Create(By.Id("password"), "Password textBox");

            public static NamedLocator ConfirmPassword
                => NamedLocator.Create(By.Id("confirmPassword"), "Confirm Password textBox");

            public static NamedLocator RegisterButton
                => NamedLocator.Create(By.XPath("//div[@role='main']//button[@type='submit']"), "Register button");
        }
    }
}
