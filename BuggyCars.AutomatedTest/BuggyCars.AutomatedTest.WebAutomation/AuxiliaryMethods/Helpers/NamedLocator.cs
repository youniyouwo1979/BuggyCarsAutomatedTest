using OpenQA.Selenium;

namespace BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers
{
    public sealed class NamedLocator
    {
        private NamedLocator(By locator, string description)
        {
            Description = description;
            Locator = locator;
        }

        public string Description { get; }

        public By Locator { get; }

        public static NamedLocator Create(By locator, string description)
        {
            return new NamedLocator(locator, description);
        }
    }
}
