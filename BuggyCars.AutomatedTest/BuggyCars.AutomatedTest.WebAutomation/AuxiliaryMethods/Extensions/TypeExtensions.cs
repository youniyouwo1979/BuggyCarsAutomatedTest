using System;
using System.IO;
using System.Linq;
using BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers;

namespace Lumos.CMS.AutomatedTests.API.AuxiliaryMethods.Extensions
{
    /// <summary>
    /// Extension for Type delegate.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Get the json text string from a specific json file path.
        /// </summary>
        /// <param name="type">The specific type.</param>
        /// <param name="paths">The remaining json file path array.</param>
        /// <returns>
        /// The json text string.
        /// </returns>
        public static string GetJsonText(this Type type, params string[] paths)
        {
            Preconditions.NotNull(type, nameof(type));
            var dllPath = type.Assembly.Location;
            var path = Path.Combine(new[] { Path.GetDirectoryName(dllPath) }.Concat(paths).ToArray());
            return File.ReadAllText(path);
        }
    }
}
