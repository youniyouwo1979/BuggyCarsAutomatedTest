using System;
using BuggyCars.AutomatedTest.WebAutomation.Configuration;
using BuggyCars.AutomatedTest.WebAutomation.Locators;

namespace BuggyCars.AutomatedTest.WebAutomation.Pages
{
    public class NavigationHeader : BasePage
    {
        private readonly AutomationSettings _settings;
        //private readonly HomePage _homePage;

        public NavigationHeader(
            AutomationSettings settings,
            BrowserBase browser,
            TestData testData)
            : base(settings, browser, testData)
        {
        }

        public void TrySignIn(string memberLogin, string password)
        {
            EnterLogin(memberLogin);
            EnterPassword(password);
            ClickLoginButton();
        }

        public void EnterLogin(string memberLogin)
        {
            ClearAndEnterText(memberLogin, ElementLocators.NavigationHeader.Login);
        }

        public void EnterPassword(string password)
        {
            ClearAndEnterText(password, ElementLocators.NavigationHeader.Password);
        }

        public void ClickLoginButton()
        {
            Click(ElementLocators.NavigationHeader.LoginButton);
        }

        public void VerifyLogoutDisplayed()
        {
            WaitForElement(ElementLocators.NavigationHeader.LogoutNavItem);
        }

        public void ClickLogout()
        {
            Click(ElementLocators.NavigationHeader.LogoutNavItem);
        }

        public void VerifyLoginButtonDisplayed()
        {
            WaitForElement(ElementLocators.NavigationHeader.LoginButton);
        }
    }
}
