using Newtonsoft.Json;

namespace BuggyCars.AutomatedTest.WebAutomation.WebDriver.Settings
{
    public class BrowserStackBuildsResponse
    {
        [JsonProperty("automation_build")]
        public AutomationBuild Automationbuild { get; set; }
    }
}
