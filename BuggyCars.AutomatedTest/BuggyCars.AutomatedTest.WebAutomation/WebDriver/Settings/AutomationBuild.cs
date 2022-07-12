using Newtonsoft.Json;

namespace BuggyCars.AutomatedTest.WebAutomation.WebDriver.Settings
{
    public class AutomationBuild
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("hashed_id")]
        public string HashedId { get; set; }
    }
}
