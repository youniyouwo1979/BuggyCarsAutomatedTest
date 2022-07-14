using System;

namespace BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers
{
    /// <summary>
    /// Represents common precondition code contract checks.
    /// </summary>
    internal static class Preconditions
    {
        /// <summary>
        /// Ensures that the specified instance is not <see langword="null"/>.
        /// </summary>
        /// <typeparam name="TInstance">The type of instance.</typeparam>
        /// <param name="instance">The instance to check.</param>
        /// <param name="parameterName">The name of the parameter the source instance came from.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="instance"/> parameter is <see langword="null"/>.</exception>
        /// <returns>The original instance specified by <paramref name="instance"/>.</returns>
        internal static TInstance NotNull<TInstance>([ValidatedNotNull] TInstance instance, string parameterName)
            where TInstance : class
        {
            return instance ?? throw new ArgumentNullException(parameterName);
        }
    }

    /// <summary>
    /// Indicates that the value of the marked element can never be <see langword="null" />.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "This annotation is only used here.")]
    internal sealed class ValidatedNotNullAttribute
        : Attribute
    {
    }
}
