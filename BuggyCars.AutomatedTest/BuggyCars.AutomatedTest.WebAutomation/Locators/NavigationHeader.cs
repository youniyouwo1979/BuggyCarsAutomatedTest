using BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers;
using OpenQA.Selenium;

namespace BuggyCars.AutomatedTest.WebAutomation.Locators
{
    public static partial class ElementLocators
    {
        internal static class NavigationHeader
        {
            public static NamedLocator Login
                => NamedLocator.Create(By.CssSelector("input[name='password']"), "Login textBox");

            public static NamedLocator Password
                => NamedLocator.Create(By.CssSelector("input[name='password']"), "Password textBox");

            public static NamedLocator LoginButton
                => NamedLocator.Create(By.CssSelector("input[name='password']"), "Login button");

            public static NamedLocator LogoutNavItem
                => NamedLocator.Create(By.CssSelector("input[name='password']"), "Login navigation item");
        }
    }
}
