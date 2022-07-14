using BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers;
using BuggyCars.AutomatedTest.WebAutomation.Configuration;
using BuggyCars.AutomatedTest.WebAutomation.Locators;
using BuggyCars.AutomatedTest.WebAutomation.Models;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BuggyCars.AutomatedTest.WebAutomation.Pages
{
    public class MemberProfilePage : BasePage
    {
        private readonly GeneralElements _generalElements;

        public MemberProfilePage(
            AutomationSettings settings,
            BrowserBase browser,
            GeneralElements generalElements,
            TestData testData)
            : base(settings, browser, testData)
        {
            _generalElements = generalElements;
        }

        public void TryUpdateMemberDetails(MemberPersonalDetails memberPersonalDetails)
        {
            Preconditions.NotNull(memberPersonalDetails, nameof(memberPersonalDetails));
            SetValueIfNotNullOrEmpty(memberPersonalDetails.FirstName, EnterFirstName);
            SetValueIfNotNullOrEmpty(memberPersonalDetails.LastName, EnterLastName);
            SetValueIfNotNullOrEmpty(memberPersonalDetails.PhoneNumber, EnterPhoneNumber);
            SetValueIfNotNullOrEmpty(memberPersonalDetails.Gender, SelectGender);
            ClickSave();
        }

        public void EnterFirstName(string givenName)
        {
            ClearAndEnterText(givenName, ElementLocators.UpdateMemberProfile.FirstName);
        }

        public void EnterLastName(string familyName)
        {
            ClearAndEnterText(familyName, ElementLocators.UpdateMemberProfile.LastName);
        }

        public void EnterPhoneNumber(string phoneNumber)
        {
            ClearAndEnterText(phoneNumber, ElementLocators.UpdateMemberProfile.Phone);
        }

        public void SelectGender(string gender)
        {
            Click(ElementLocators.UpdateMemberProfile.Gender(gender));
        }

        public void ClickSave()
        {
            Click(ElementLocators.UpdateMemberProfile.SaveButton);
        }

        public void VerifyPhoneNumber(string expectedPhone)
        {
            Assert.AreEqual(expectedPhone, GetWebElement(ElementLocators.UpdateMemberProfile.Phone).GetAttribute("value"), $"The phone should be {expectedPhone}!");
        }
    }
}
