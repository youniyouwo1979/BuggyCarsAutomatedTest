using System;
using System.Collections.Generic;
using System.Linq;
using BuggyCars.AutomatedTest.WebAutomation.WebDriver.Browserstack;
using BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers;
using Newtonsoft.Json;
using OpenQA.Selenium.Remote;

namespace BuggyCars.AutomatedTest.WebAutomation.WebDriver.Settings
{
    public class BrowserStackInfo
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BrowserStackInfo(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            BuildName = $"{DateTime.Now:dd/MM/yyyy HH:mm:ss}";
        }

        public string BuildName { get; set; }

        public int QueueNumber { get; set; }

        public string GetBuildId() => GetBuildsList().FirstOrDefault(b => b.Automationbuild.Name == GenerateBrowserStackBuildName())?.Automationbuild.HashedId;

        public int GetQueuedSessions()
        {
            return GetAutomationPlanDetails().QueuedSessions;
        }

        public string GenerateBrowserStackBuildName() => BuildName;

        public int GenerateBrowserStackQueue() => QueueNumber;

        public string GetVideoUrl(RemoteWebDriver driver, string browserName = "")
        {
            Preconditions.NotNull(driver, nameof(driver));
            Preconditions.NotNull(browserName, nameof(browserName));

            if (!browserName.Contains("BrowserStack", StringComparison.Ordinal))
            {
                return "It's not a BrowserStack driver, will not have a video link to it.";
            }

            var sessionId = driver.SessionId.ToString();
            var buildsId = GetBuildId();
            return $"https://automate.browserStack.com/builds/{buildsId}/sessions/{sessionId}";
        }

        internal IEnumerable<BrowserStackBuildsResponse> GetBuildsList(int limit = 5)
        {
            var buildInformation = GetBrowserStackInfo<List<BrowserStackBuildsResponse>>($"https://api.browserStack.com/automate/builds.json?limit={limit}");
            return buildInformation;
        }

        internal AutomationPlanDetails GetAutomationPlanDetails()
        {
            var planInformation = GetBrowserStackInfo<AutomationPlanDetails>("https://api.browserStack.com/automate/plan.json");
            return planInformation;
        }

        private T GetBrowserStackInfo<T>(string url)
        {
            var httpClient = _httpClientFactory.GetHttpClient();
            var json = httpClient.GetStringAsync(url).GetAwaiter().GetResult();
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
