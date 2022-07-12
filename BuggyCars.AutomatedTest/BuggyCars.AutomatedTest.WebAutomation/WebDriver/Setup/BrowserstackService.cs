using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using BrowserStack;
using log4net;
using BuggyCars.AutomatedTest.WebAutomation.Configuration;
using BuggyCars.AutomatedTest.WebAutomation.WebDriver.Settings;
using BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers;

namespace BuggyCars.AutomatedTest.WebAutomation.WebDriver.Setup
{
    public class BrowserStackService
    {
        private static readonly object _browserStackCheckAvailableSessionsLock = new();
        private readonly AutomationSettings _settings;
        private readonly BrowserStackInfo _browserStackInfo;

        public BrowserStackService(AutomationSettings settings, BrowserStackInfo browserStackInfo)
        {
            _settings = settings;
            _browserStackInfo = browserStackInfo;
        }

        public static ILog Logger { get; } = Log4NetHelper.GetLogger(typeof(Log4NetHelper));

        public static Local BrowserStackProxyLocal { get; set; }

        public static void StopBrowserStackBinary() => BrowserStackProxyLocal.stop();

        public void LaunchBrowserStackBinary()
        {
            var settings = _settings.GetBrowserStackKeys();
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                case PlatformID.Win32NT:
                    var programFilesPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
                    var browserStackBinaryPath = _settings.IsOctopusDeployment ? Path.GetFullPath(Path.Combine(programFilesPath, @"BrowserStackLocal", @"BrowserStackLocal.exe")) : string.Empty;
                    settings.Add(new KeyValuePair<string, string>("binarypath", browserStackBinaryPath));
                    break;
            }

            LocalProcessHelper.StartLocalProcess("BrowserStackLocal", () =>
            {
                BrowserStackProxyLocal = new Local();
                BrowserStackProxyLocal.start(settings);
            });
        }

        public void CheckForAvailableBrowserStackSessions(int timeout)
        {
            var browserStackCheckAvailableSessionsLock = _browserStackCheckAvailableSessionsLock;

            // Lock to stop other threads trying to take the session
            Monitor.Enter(browserStackCheckAvailableSessionsLock);
            try
            {
                const int pollingTime = 20;
                var elapsedTime = 0;
                var sessionAvailable = false;
                while (elapsedTime < timeout)
                {
                    var queuedSessions = _browserStackInfo.GetQueuedSessions();
                    sessionAvailable = queuedSessions < 1;
                    if (sessionAvailable)
                    {
                        Logger.Debug($"Number of Queued Sessions is {queuedSessions}. session is available");
                        break;
                    }

                    Logger.Debug($"Number of Queued Sessions is {queuedSessions} Waiting {pollingTime} seconds before trying again");
                    Thread.Sleep(pollingTime);
                    elapsedTime += pollingTime;
                }

                if (!sessionAvailable)
                {
                    Logger.Error($"No BrowserStack sessions available during timeout period: {timeout} seconds");
                    throw new TimeoutException("No BrowserStack sessions available.");
                }
            }
            finally
            {
                Monitor.Exit(browserStackCheckAvailableSessionsLock);
            }
        }
    }
}
