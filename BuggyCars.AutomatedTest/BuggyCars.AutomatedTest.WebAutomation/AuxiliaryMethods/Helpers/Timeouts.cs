using System;

namespace BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers
{
    public static class Timeouts
    {
        /// <summary>
        /// Gets standard timeout for waiting until loaded state occurs.
        /// </summary>
        public static TimeSpan LoadingWait => TimeSpan.FromSeconds(60);
    }
}
