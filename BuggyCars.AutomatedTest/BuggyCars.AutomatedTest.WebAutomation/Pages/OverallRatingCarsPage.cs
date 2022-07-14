using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BuggyCars.AutomatedTest.WebAutomation.Configuration;
using BuggyCars.AutomatedTest.WebAutomation.Locators;
using NUnit.Framework;

namespace BuggyCars.AutomatedTest.WebAutomation.Pages
{
    public class OverallRatingCarsPage : BasePage
    {
        public OverallRatingCarsPage(
            AutomationSettings settings,
            BrowserBase browser,
            TestData testData)
            : base(settings, browser, testData)
        {
        }

        public void VerifyRankingForAllCars()
        {
            var pageText = GetText(ElementLocators.CarRanking.Pages);
            pageText = pageText.Trim();
            int numOfPages = int.Parse(pageText[pageText.Length - 1].ToString());
            for (int i = 0; i < numOfPages; i++)
            {
                List<int> rankList = new();
                List<int> rankListToSort = new();
                List<int> votesList = new();
                List<int> votesListToSort = new();
                var rankingRows = FindAllElements(ElementLocators.CarRanking.RankingRows);
                for (int j = 1; j < rankingRows.ToList().Count; j++)
                {
                    var intRank = Convert.ToInt32(GetText(ElementLocators.CarRanking.Rank(j)));
                    rankList.Add(intRank);
                    rankListToSort.Add(intRank);
                    var intVotes = Convert.ToInt32(GetText(ElementLocators.CarRanking.Votes(j)));
                    votesList.Add(intVotes);
                    votesListToSort.Add(intVotes);
                }
                rankListToSort.Sort();
                votesListToSort = votesListToSort.OrderBy(x => -x).ToList();
                CollectionAssert.AreEqual(rankListToSort, rankList, "The values of Rank column are not sorted with highest rank for the cars");
                CollectionAssert.AreEqual(votesListToSort, votesList, "The values of Votes column are not sorted with highest votes for the cars");
                if (i != (numOfPages - 1))
                {
                    Click(ElementLocators.CarRanking.NextPageSymbol);
                    WaitLow();
                }
            }
        }
    }
}
