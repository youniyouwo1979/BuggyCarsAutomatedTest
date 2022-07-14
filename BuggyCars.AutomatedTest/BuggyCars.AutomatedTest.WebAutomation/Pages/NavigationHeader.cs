using System;
using BuggyCars.AutomatedTest.WebAutomation.Configuration;
using BuggyCars.AutomatedTest.WebAutomation.Locators;

namespace BuggyCars.AutomatedTest.WebAutomation.Pages
{
    public class NavigationHeader : BasePage
    {
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

        public void ClickLogo()
        {
            Click(ElementLocators.NavigationHeader.LogoLink);
        }

        public void VerifyLogoutDisplayed()
        {
            WaitForElement(ElementLocators.NavigationHeader.LogoutLink);
        }

        public void ClickLogout()
        {
            Click(ElementLocators.NavigationHeader.LogoutLink);
        }

        public void VerifyLoginButtonDisplayed()
        {
            WaitForElement(ElementLocators.NavigationHeader.LoginButton);
        }

        public void ClickRegister()
        {
            Click(ElementLocators.NavigationHeader.RegisterLink);
        }

        public void ClickProfile()
        {
            Click(ElementLocators.NavigationHeader.ProfileLink);
        }
    }
}
