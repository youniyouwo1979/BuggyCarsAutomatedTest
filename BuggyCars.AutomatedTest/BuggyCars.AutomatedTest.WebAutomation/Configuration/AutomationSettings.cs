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
        BrowserStack
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

        public string HomePageUrl { get; set; }
    }
}
