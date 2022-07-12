using System;

namespace BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers
{
    public static class Timeouts
    {
        /// <summary>
        /// Gets standard timeout for waiting until loaded state occurs.
        /// </summary>
        public static TimeSpan LoadingWait => TimeSpan.FromSeconds(60);

        /// <summary>
        /// Gets timeout wait where the presence of the element is not important, e.g it could have appeared and disappeared before it's presence is detected. Use case a button processing spinner.
        /// </summary>
        public static TimeSpan LoadingTryWait => TimeSpan.FromSeconds(10);

        /// <summary>
        /// Gets timeout for waiting until seat allocation lock is available. Used in parallel execution so only one thread will move through the order seat allocation flow at a time. It prevents multiple threads trying to reserve the same seats.
        /// </summary>
        public static TimeSpan SeatAllocationThreadSafeWait => TimeSpan.FromSeconds(120);
    }
}
