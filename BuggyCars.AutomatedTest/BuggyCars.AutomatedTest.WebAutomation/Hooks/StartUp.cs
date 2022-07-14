using System;
using BoDi;
using Lumos.CMS.AutomatedTests.API.AuxiliaryMethods.Extensions;
using BuggyCars.AutomatedTest.WebAutomation.Configuration;
using Newtonsoft.Json;
//using BuggyCars.AutomatedTest.WebAutomation.WebDriver.Browserstack;
//using BuggyCars.AutomatedTest.WebAutomation.WebDriver.Setup;
//using BuggyCars.AutomatedTest.WebAutomation.WebDriver.Settings;
using BuggyCars.AutomatedTest.WebAutomation.WebDriver.LocalBrowser;
using BuggyCars.AutomatedTest.WebAutomation.WebDriver;

namespace BuggyCars.AutomatedTest.WebAutomation.Hooks
{
    /// <summary>
    /// This class registers the dependencies for specflow run.
    /// </summary>
    public static class StartUp
    {
        /// <summary>
        /// Register the dependencies.
        /// </summary>
        /// <param name="objectContainer">the object container.</param>
        public static void RegisterDependencies(IObjectContainer objectContainer)
        {
            _ = objectContainer ?? throw new ArgumentNullException(nameof(objectContainer));

            // Load settings into singleton
            objectContainer.RegisterFactoryAs(_ =>
            {
                var jsonText = typeof(StartUp).GetJsonText("Configuration", "AutomationSettings.json");
                var settings = JsonConvert.DeserializeObject<AutomationSettings>(jsonText);

                return settings;
            });

            objectContainer.RegisterFactoryAs(_ =>
            {
                var jsonText = typeof(StartUp).GetJsonText("Configuration", "StringMessages.json");
                var settings = JsonConvert.DeserializeObject<StringMessages>(jsonText);

                return settings;
            });

            objectContainer.RegisterFactoryAs(_ =>
            {
                var jsonText = typeof(StartUp).GetJsonText("Configuration", "TestData.json");
                var settings = JsonConvert.DeserializeObject<TestData>(jsonText);

                return settings;
            });

            //// Http client factory
            //objectContainer.RegisterFactoryAs<IHttpClientFactory>(o =>
            //{
            //    var httpClient = new HttpClient();
            //    var settings = o.Resolve<AutomationSettings>();
            //    var byteArray = Encoding.ASCII.GetBytes($"{settings.BrowserStackUser}:{settings.BrowserStackKey}");
            //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            //    return new HttpClientFactory(httpClient);
            //});

            //objectContainer.RegisterFactoryAs<IBrowserStackSettingsFactory>(o =>
            //{
            //    return new BrowserStackSettingsFactory(o.Resolve<AutomationSettings>());
            //});

            //objectContainer.RegisterFactoryAs<IBrowserStackDriverFactory>(o =>
            //{
            //    return new BrowserStackDriverFactory(
            //        o.Resolve<IBrowserStackSettingsFactory>(),
            //        o.Resolve<BrowserStackService>(),
            //        o.Resolve<AutomationSettings>());
            //});

            //objectContainer.RegisterFactoryAs<BrowserStackService>(o =>
            //{
            //    return new BrowserStackService(
            //        o.Resolve<AutomationSettings>(),
            //        o.Resolve<BrowserStackInfo>());
            //});

            //objectContainer.RegisterFactoryAs<BrowserStackInfo>(o =>
            //{
            //    return new BrowserStackInfo(
            //        o.Resolve<IHttpClientFactory>());
            //});

            objectContainer.RegisterFactoryAs<ILocalBrowserDriverFactory>(o =>
            {
                return new LocalBrowserDriverFactory(o.Resolve<ILocalBrowserSettingsFactory>());
            });

            objectContainer.RegisterFactoryAs<ILocalBrowserSettingsFactory>(o =>
            {
                return new LocalBrowserSettingsFactory(o.Resolve<AutomationSettings>());
            });

            objectContainer.RegisterFactoryAs<IWebDriverFactory>(o =>
            {
                return new WebDriverFactory(
                    o.Resolve<AutomationSettings>(),
                    //o.Resolve<IBrowserStackDriverFactory>(),
                    o.Resolve<ILocalBrowserDriverFactory>());
            });
        }
    }
}
