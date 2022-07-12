using System.Collections.Generic;
using BuggyCars.AutomatedTest.WebAutomation.Configuration;
using BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers;

namespace BuggyCars.AutomatedTest.WebAutomation.WebDriver.Browserstack.Settings
{
    public static class BrowserStackBaseSettings
    {
        public static Dictionary<string, object> GetSetDefaultDriverOptions(AutomationSettings settings)
        {
            Preconditions.NotNull(settings, nameof(settings));

            return new Dictionary<string, object>
            {
                { "userName", settings.BrowserStackUser },
                { "accessKey", settings.BrowserStackKey },
                { "local", true },
                { "debug", true },
                { "consoleLogs", "warnings" },
                { "timezone", "Auckland" },
                { "projectName", settings.RemoteExecutionProjectName },
                { "buildName", settings.RemoteExecutionBuildName },
                { "networkLogs", true }
            };
        }
    }
}
