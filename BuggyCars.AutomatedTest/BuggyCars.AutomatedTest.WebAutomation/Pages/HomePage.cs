using System;
using BuggyCars.AutomatedTest.WebAutomation.Configuration;
using BuggyCars.AutomatedTest.WebAutomation.Locators;

namespace BuggyCars.AutomatedTest.WebAutomation.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(
            AutomationSettings settings,
            BrowserBase browser,
            TestData testData)
            : base(settings, browser, testData)
        {
        }

        public void NavigateToHomePage()
        {
            var homePageUrl = Settings.HomePageUrl;
            NavigateToUrlAndWaitUntilLoaded(homePageUrl, ElementLocators.Home.PopularMakeImage);
        }

        public void SelectPopularModel()
        {
            Click(ElementLocators.Home.PopularModelImage);
        }

        public void SelectOverallRating()
        {
            Click(ElementLocators.Home.OveralRatingImage);
        }
    }
}
