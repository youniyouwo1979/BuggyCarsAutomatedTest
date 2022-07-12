using Newtonsoft.Json;

namespace BuggyCars.AutomatedTest.WebAutomation.WebDriver.Settings
{
    public class AutomationPlanDetails
    {
        [JsonProperty("automate_plan")]
        public string PlanName { get; set; }

        [JsonProperty("parallel_sessions_running")]
        public int ParallelSessionsRunning { get; set; }

        [JsonProperty("team_parallel_sessions_max_allowed")]
        public int TeamParallelSessionsMaxAllowed { get; set; }

        [JsonProperty("parallel_sessions_max_allowed")]
        public int ParallelSessionsMaxAlloed { get; set; }

        [JsonProperty("queued_sessions")]
        public int QueuedSessions { get; set; }

        [JsonProperty("queued_sessions_max_allowed")]
        public int QueuedSessionsMaxAllowed { get; set; }
    }
}
