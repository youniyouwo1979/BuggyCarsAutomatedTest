using System;
using BuggyCars.AutomatedTest.WebAutomation.Configuration;
using BuggyCars.AutomatedTest.WebAutomation.Locators;
using NUnit.Framework;

namespace BuggyCars.AutomatedTest.WebAutomation.Pages
{
    public class CarPage : BasePage
    {
        public CarPage(
            AutomationSettings settings,
            BrowserBase browser,
            TestData testData)
            : base(settings, browser, testData)
        {
        }

        public string GetCarModel()
        {
            return GetText(ElementLocators.CarModel.ModelName);
        }

        public int GetTotalVotes()
        {
            var intVotes = Convert.ToInt32(GetText(ElementLocators.CarModel.Votes));
            return intVotes;
        }

        public void EnterComment(string comment)
        {
            ClearAndEnterText(comment, ElementLocators.CarModel.Comment);
        }

        public void VoteWithComment(string comment)
        {
            EnterComment(comment);
            ClickVote();
        }
        public void ClickVote()
        {
            Click(ElementLocators.CarModel.VoteButton);
        }

        public void VerifyVotesIncreased(int oldVotes)
        {
            Assert.Greater(GetTotalVotes(), oldVotes);
        }
    }
}
