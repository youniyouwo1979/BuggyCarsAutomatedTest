using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using NUnit.Framework;

namespace BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers
{
    public class ExceptionHandler
    {
        private readonly ILog _logger = Log4NetHelper.GetLogger(typeof(ExceptionHandler));

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
            _logger.Error($"Test failed. Reason: {e.Message}");
            Assert.Fail($"Test failed. Reason: {e.Message}");
        }
    }
}
