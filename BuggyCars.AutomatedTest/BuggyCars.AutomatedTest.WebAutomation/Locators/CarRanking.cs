using BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers;
using OpenQA.Selenium;

namespace BuggyCars.AutomatedTest.WebAutomation.Locators
{
    public static partial class ElementLocators
    {
        internal static class CarRanking
        {
            public static NamedLocator RankingRows
                => NamedLocator.Create(By.XPath("//table//tr"), "list of rows in ranking table");

            public static NamedLocator Rank(int row)
                => NamedLocator.Create(By.XPath($"(//table//tr)[{row + 1}]/*[4]"), $"Rank for the row {row}");

            public static NamedLocator CarImage(int row)
                => NamedLocator.Create(By.XPath($"(//table//tr)[{row + 1}]/*[1]//img"), $"Car image for the row {row}");

            public static NamedLocator Votes(int row)
                => NamedLocator.Create(By.XPath($"(//table//tr)[{row + 1}]/*[5]"), $"Votes for the row {row}");

            public static NamedLocator Pages
                => NamedLocator.Create(By.XPath("//my-pager//div[@class='pull-xs-right']"), "Pages");

            public static NamedLocator NextPageSymbol
                => NamedLocator.Create(By.XPath("//my-pager//div[@class='pull-xs-right']/*[3]"), "Next page symbol");

        }
    }
}
