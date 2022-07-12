using System.Collections.Generic;

namespace BuggyCars.AutomatedTest.WebAutomation.Configuration
{
    public enum BrowserHost
    {
        /// <summary>
        /// Local Machine Browser.
        /// </summary>
        Local,

        /// <summary>
        /// BrowserStack Browsers.
        /// </summary>
        BrowserStack,

        /// <summary>
        /// Docker Selenium Browsers.
        /// </summary>
        Docker
    }

    public enum BrowserName
    {
        /// <summary>
        /// Windows Chrome browser.
        /// </summary>
        ChromeDesktop,

        /// <summary>
        /// Windows Firefox browser.
        /// </summary>
        FirefoxDesktop,

        /// <summary>
        /// Windows Edge browser.
        /// </summary>
        EdgeDesktop,

        /// <summary>
        /// Edge Kiosk browser.
        /// </summary>
        EdgeKiosk,

        /// <summary>
        /// Safari desktop browser.
        /// </summary>
        SafariDesktop,

        /// <summary>
        /// Windows Edge browser.
        /// </summary>
        SamsungS21Chrome,

        /// <summary>
        /// Windows Edge browser.
        /// </summary>
        Iphone12Safari,

        /// <summary>
        /// Windows Edge browser.
        /// </summary>
        Iphone12ProSafari
    }

    public class AutomationSettings
    {
        public BrowserHost BrowserHost { get; set; }

        public BrowserName Browser { get; set; }

        public string BrowserStackUser { get; set; }

        public string BrowserStackKey { get; set; }

        public bool IsOctopusDeployment { get; set; }

        public string RemoteExecutionProjectName { get; set; }

        public string RemoteExecutionBuildName { get; set; }

        public string VmLogHistoryPath { get; set; }

        public string CaptchaSiteKey { get; set; }

        public string SignInGrantType { get; set; }

        public string DockerSeleniumHubUrl { get; set; }

        public string HomePageUrl { get; set; }

        public List<KeyValuePair<string, string>> GetBrowserStackKeys()
        {
            return new() { new("key", BrowserStackKey) };
        }
    }
}
