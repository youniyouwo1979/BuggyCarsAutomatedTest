using Newtonsoft.Json;

namespace BuggyCars.AutomatedTest.WebAutomation.WebDriver.Settings
{
    public class BrowserStackPlanResponse
    {
        [JsonProperty("automation_plan")]
        public AutomationPlanDetails AutomationPlanDetails { get; set; }
    }
}
