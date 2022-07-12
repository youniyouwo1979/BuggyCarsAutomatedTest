using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using log4net;
using BuggyCars.AutomatedTest.WebAutomation.WebDriver.Setup;

namespace BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers
{
    public static class LocalProcessHelper
    {
        private static readonly object _startProcessLock = new();
        private static readonly HashSet<string> _startedProcessNames = new();

        private static ILog Logger { get; } = Log4NetHelper.GetLogger(typeof(Log4NetHelper));

        public static void StartLocalProcess(string processName, Action startLocalProcessAction)
        {
            Preconditions.NotNull(startLocalProcessAction, nameof(startLocalProcessAction));

            // Need to have lock here, otherwise multiple tests can try to start the process at the same time
            Logger.Debug($"{processName} local check");
            try
            {
                Monitor.Enter(_startProcessLock);
                if (IsLocalProcessRunning(processName))
                {
                    Logger.Info($"The {processName} process is already running. Don't need to launch again.");
                    return;
                }

                // Start the local process
                startLocalProcessAction();
                _startedProcessNames.Add(processName);
                Logger.Debug($"Launched {processName} local binary.");
            }
            finally
            {
                Monitor.Exit(_startProcessLock);
            }
        }

        public static void CloseLocalProcesses()
        {
            foreach (var processName in _startedProcessNames)
            {
                if (IsLocalProcessRunning(processName))
                {
                    if (processName == "BrowserStackLocal")
                    {
                        Logger.Debug("Stopping BrowserStack Binary");
                        BrowserStackService.StopBrowserStackBinary();
                    }
                    else
                    {
                        Logger.Debug($"Closing existing {processName} process.");
                        Process.GetProcessesByName(processName)[0].Kill();
                    }
                }
            }
        }

        private static bool IsLocalProcessRunning(string processName)
        {
            var processes = Process.GetProcessesByName(processName);
            Logger.Debug($"Check {processName} running status: process exist = {processes.Length}");
            return processes.Length > 0;
        }
    }
}
