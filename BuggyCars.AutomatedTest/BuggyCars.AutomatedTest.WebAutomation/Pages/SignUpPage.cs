using System;
using BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers;
using BuggyCars.AutomatedTest.WebAutomation.Configuration;
using BuggyCars.AutomatedTest.WebAutomation.Locators;
using BuggyCars.AutomatedTest.WebAutomation.Models;

namespace BuggyCars.AutomatedTest.WebAutomation.Pages
{
    public class SignUpPage : BasePage
    {
        private readonly GeneralElements _generalElements;

        public SignUpPage(
            AutomationSettings settings,
            BrowserBase browser,
            GeneralElements generalElements,
            TestData testData)
            : base(settings, browser, testData)
        {
            _generalElements = generalElements;
        }

        public void EnterFirstName(string firstName)
        {
            ClearAndEnterText(firstName, ElementLocators.SignUp.FirstName);
        }

        public void EnterLastName(string lastName)
        {
            ClearAndEnterText(lastName, ElementLocators.SignUp.LastName);
        }

        public void EnterLogin(string login)
        {
            ClearAndEnterText(login, ElementLocators.SignUp.Login);
        }

        public void EnterPassword(string password)
        {
            ClearAndEnterText(password, ElementLocators.SignUp.Password);
        }

        public void EnterConfirmPassword(string confirmPassword)
        {
            ClearAndEnterText(confirmPassword, ElementLocators.SignUp.ConfirmPassword);
        }

        public void ClickRegisterButton() => Click(ElementLocators.SignUp.RegisterButton);

        public void EnterSignUpDetails(SignUpDetails signUpDetails)
        {
            Preconditions.NotNull(signUpDetails, nameof(signUpDetails));
            SetValueIfNotNullOrEmpty(signUpDetails.Login, EnterLogin);
            SetValueIfNotNullOrEmpty(signUpDetails.Name.FirstName, EnterFirstName);
            SetValueIfNotNullOrEmpty(signUpDetails.Name.LastName, EnterLastName);
            SetValueIfNotNullOrEmpty(signUpDetails.Password, EnterPassword);
            SetValueIfNotNullOrEmpty(signUpDetails.Password, EnterConfirmPassword);
        }

        public void TrySignUp(SignUpDetails signupDetails)
        {
            EnterSignUpDetails(signupDetails);
            ClickRegisterButton();
        }
    }
}
