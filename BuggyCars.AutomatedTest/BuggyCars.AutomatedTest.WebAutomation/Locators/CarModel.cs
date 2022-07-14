using BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers;
using OpenQA.Selenium;

namespace BuggyCars.AutomatedTest.WebAutomation.Locators
{
    public static partial class ElementLocators
    {
        internal static class CarModel
        {
            public static NamedLocator ModelName
                => NamedLocator.Create(By.XPath("(//div[@class='row'])[3]/*[2]"), "Car model name");

            public static NamedLocator Comment
                => NamedLocator.Create(By.Id("comment"), "Comment textBox");

            public static NamedLocator VoteButton
                => NamedLocator.Create(By.CssSelector("button[class='btn btn-success']"), "Vote button");

            public static NamedLocator Votes
                => NamedLocator.Create(By.XPath("(//div[@class='card'])[2]//strong"), "Votes count");
        }
    }
}
