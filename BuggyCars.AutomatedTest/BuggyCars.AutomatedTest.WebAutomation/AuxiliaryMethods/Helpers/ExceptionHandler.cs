using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuggyCars.AutomatedTest.WebAutomation.Pages;
using log4net;
using NUnit.Framework;

namespace BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers
{
    public class ExceptionHandler
    {
        private readonly ILog _logger = Log4NetHelper.GetLogger(typeof(ExceptionHandler));
        private readonly BrowserBase _browser;

        public ExceptionHandler(BrowserBase browser)
        {
            _browser = browser;
        }

        /// <summary>
        /// Executes the action, logs the errors and fails the test.when there is an exception.
        /// </summary>
        /// <param name="action">The action to be executed.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Catch-all is deliberate.")]
        public void Execute(Action action)
        {
            Preconditions.NotNull(action, nameof(action));
            try
            {
                action();
            }
            catch (Exception e)
            {
                LogErrorAndFailTheTest(e);
            }
        }

        /// <summary>
        /// Logs the error and fail the test.
        /// </summary>
        /// <param name="e">The exception to be logged.</param>
        private void LogErrorAndFailTheTest(Exception e)
        {
            Preconditions.NotNull(e, nameof(e));
            var screenshotName = TestContext.CurrentContext.Test.Name;
            screenshotName = string.Concat(screenshotName.Split(Path.GetInvalidFileNameChars()));
            var date = DateTime.Now;
            var formattedDate = date.ToString("dd-MM_HH-mm", CultureInfo.InvariantCulture);
            var screenshotNameWithCurrentTime = screenshotName + "_" + formattedDate;
            var pageUrl = _browser.GetPageUrl();
            var screenshotPath = _browser.TakeScreenshot(screenshotNameWithCurrentTime);
            _logger.Error($"Test failed. Reason: {e.Message} .\n Screenshot: {screenshotPath} .\n Page url: {pageUrl}");
            Assert.Fail($"Test failed. Reason: {e.Message} .\n Screenshot: {screenshotPath} .\n Page url: {pageUrl}");
        }
    }
}
