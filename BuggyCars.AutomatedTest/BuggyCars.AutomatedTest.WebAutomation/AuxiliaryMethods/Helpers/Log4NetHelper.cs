using System;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using ReportPortal.Log4Net;

namespace BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers
{
    /// <summary>
    /// This class provides a logging utility to log different levels of messages.
    /// </summary>
    public static class Log4NetHelper
    {
        private const string Layout = "%date{dd MMM yyyy HH:mm:ss} - %level - %message %newline";

        private static ConsoleAppender _consoleAppender;
        private static FileAppender _fileAppender;
        private static RollingFileAppender _rollingFileAppender;
        private static ReportPortalAppender _reportAppender;

        private static ILog Logger { get; set; }

        /// <summary>
        /// Gets the logger instance.
        /// </summary>
        /// <param name="type">The full name of type will be used as the name of the logger to retrieve.</param>
        /// <returns>
        /// The logger instance.
        /// </returns>
        public static ILog GetLogger(Type type)
        {
            _consoleAppender ??= GetConsoleAppender();
            _fileAppender ??= GetFileAppender();
            _rollingFileAppender ??= GetRollingFileAppender();
            _reportAppender ??= GetReportAppender();

            if (Logger != null)
            {
                return Logger;
            }

            BasicConfigurator.Configure(_consoleAppender, _fileAppender, _rollingFileAppender, _reportAppender);
            Logger = LogManager.GetLogger(type);
            return Logger;
        }

        /// <summary>
        /// Gets a flexible layout configurable with pattern string.
        /// </summary>
        /// <returns>
        /// A flexible pattern layout.
        /// </returns>
        private static PatternLayout GetPatternLayout()
        {
            var patternLayout = new PatternLayout
            {
                ConversionPattern = Layout
            };
            patternLayout.ActivateOptions();
            return patternLayout;
        }

        /// <summary>
        /// Gets a console appender which appends logging events to the console.
        /// </summary>
        /// <returns>
        /// A console appender.
        /// </returns>
        private static ConsoleAppender GetConsoleAppender()
        {
            var consoleAppender = new ConsoleAppender
            {
                Name = "consoleAppender",
                Layout = GetPatternLayout(),
                Threshold = Level.All
            };
            consoleAppender.ActivateOptions();
            return consoleAppender;
        }

        /// <summary>
        /// Gets a file appender which appends logging events to a file.
        /// </summary>
        /// <returns>
        /// A file appender.
        /// </returns>
        private static FileAppender GetFileAppender()
        {
            var fileAppender = new FileAppender
            {
                Name = "fileAppender",
                Layout = GetPatternLayout(),
                Threshold = Level.All,
                AppendToFile = false,
                File = "Logs\\Log.log"
            };
            fileAppender.ActivateOptions();
            return fileAppender;
        }

        /// <summary>
        /// Gets a report portal appender which pushes logs to the report portal.
        /// </summary>
        /// <returns>
        /// A report portal appender.
        /// </returns>
        private static ReportPortalAppender GetReportAppender()
        {
            var reportAppender = new ReportPortalAppender
            {
                Name = "RP",
                Layout = GetPatternLayout(),
                Threshold = Level.All
            };
            reportAppender.ActivateOptions();
            return reportAppender;
        }

        /// <summary>
        /// Gets a rolling file appender which rolls log files based on size or date.
        /// </summary>
        /// <returns>
        /// A rolling file appender.
        /// </returns>
        private static RollingFileAppender GetRollingFileAppender()
        {
            var rollingAppender = new RollingFileAppender
            {
                Name = "Rolling File Appender",
                Layout = GetPatternLayout(),
                Threshold = Level.All,
                AppendToFile = true,
                File = "Logs\\RollingFileLogger.log",
                MaximumFileSize = "5000MB",
                MaxSizeRollBackups = 15
            };
            rollingAppender.ActivateOptions();
            return rollingAppender;
        }
    }
}
