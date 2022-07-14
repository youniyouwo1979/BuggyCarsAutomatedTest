using BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers;
using OpenQA.Selenium;

namespace BuggyCars.AutomatedTest.WebAutomation.Locators
{
    public static partial class ElementLocators
    {
        internal static class NavigationHeader
        {
            public static NamedLocator Login
                => NamedLocator.Create(By.CssSelector("input[name='login']"), "Login textBox");

            public static NamedLocator Password
                => NamedLocator.Create(By.CssSelector("input[name='password']"), "Password textBox");

            public static NamedLocator LoginButton
                => NamedLocator.Create(By.XPath("//nav[contains(@class, 'navbar')]//button[@type='submit']"), "Login button");

            public static NamedLocator RegisterLink
                => NamedLocator.Create(By.XPath("//nav[contains(@class, 'navbar')]//a[contains(@href, 'register')]"), "Register link");

            public static NamedLocator LogoutLink
                => NamedLocator.Create(By.CssSelector("a[href='javascript:void(0)']"), "Logout link");

            public static NamedLocator ProfileLink
                => NamedLocator.Create(By.CssSelector("a[href='/profile']"), "Profile link");

            public static NamedLocator LogoLink
                => NamedLocator.Create(By.CssSelector(".navbar-brand"), "Logo link");
        }
    }
}
