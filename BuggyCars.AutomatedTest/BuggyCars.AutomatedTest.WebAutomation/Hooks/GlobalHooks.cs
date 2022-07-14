using log4net;
using BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers;
using BuggyCars.AutomatedTest.WebAutomation.Pages;
using TechTalk.SpecFlow;
using System;

namespace BuggyCars.AutomatedTest.WebAutomation.Hooks
{
    /// <summary>
    /// This class includes the Specflow setup.
    /// </summary>
    [Binding]
    public class GlobalHooks
    {
        private static readonly ILog _logger = Log4NetHelper.GetLogger(typeof(GlobalHooks));
        private readonly BrowserBase _browser;

        public GlobalHooks(BrowserBase browser)
        {
            _browser = browser;
        }

        /// <summary>
        /// Logs the debug message before the entire test run.
        /// </summary>
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            _logger.Debug("XXXXXXXXXXXXXXX        STARTING TEST RUN      XXXXXXXXXXXXXXX");
            BrowserBase.CreateScreenshotDirectory();
        }

        /// <summary>
        /// Logs the debug message after the entire test run.
        /// </summary>
        [AfterTestRun]
        public static void AfterTestRun()
        {
            _logger.Debug("XXXXXXXXXXXXXXX        AFTER TESTS      XXXXXXXXXXXXXXX");
        }

        /// <summary>
        /// Registers the dependencies before executing each feature.
        /// </summary>
        /// <param name="featureContext">The feature context.</param>
        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            Preconditions.NotNull(featureContext, nameof(featureContext));
            _logger.Debug("Registering Dependencies");
            StartUp.RegisterDependencies(featureContext.FeatureContainer);

            var name = featureContext.FeatureInfo.Title;
            _logger.Debug("XXXXXXXXXXXXXXX        STARTING FEATURE TESTS - " + name + "     XXXXXXXXXXXXXXX");
        }

        /// <summary>
        /// Logs the debug message before executing each scenario.
        /// </summary>
        /// <param name="scenarioContext">The scenario context.</param>
        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            _logger.Debug("BeforeScenario");
            Preconditions.NotNull(scenarioContext, nameof(scenarioContext));
            var name = scenarioContext.ScenarioInfo.Title;
            _logger.Debug("XXXXXXXXXXXXXXX        STARTING SCENARIO - " + name + "     XXXXXXXXXXXXXXX");
            try
            {
                _logger.Debug("Starting browser");
                _browser.SetUp();
            }
            catch (Exception e)
            {
                _logger.Error(e.Message + " Failed to open browser");
                throw new InvalidOperationException("Failed to open browser");
            }
        }

        /// <summary>
        /// Logs the debug message before executing each step.
        /// </summary>
        /// <param name="scenarioContext">The scenario context.</param>
        [BeforeStep]
        public void BeforeStep(ScenarioContext scenarioContext)
        {
            Preconditions.NotNull(scenarioContext, nameof(scenarioContext));
            var stepKeyword = scenarioContext.StepContext.StepInfo.StepDefinitionType;
            var stepName = scenarioContext.StepContext.StepInfo.Text;
            _logger.Debug($"XXXXXXXXXXXXXXX     STARTING STEP - {stepKeyword} {stepName}     XXXXXXXXXXXXXXX");
        }

        /// <summary>
        /// Logs the debug message after executing each step.
        /// </summary>
        [AfterStep]
        public void AfterStep()
        {
            _logger.Info($"XXXXXXXXXXXXXXX     STEP - Finished     XXXXXXXXXXXXXXX");
        }

        /// <summary>
        /// Logs the debug message after executing each scenario.
        /// </summary>
        [AfterScenario]
        public void AfterScenario()
        {
            _logger.Debug("XXXXXXXXXXXXXXX        FINISHING SCENARIO      XXXXXXXXXXXXXXX");
            //_browser.BrowserStackMarkSessionStatus();
            _browser.Dispose();
        }
    }
}
