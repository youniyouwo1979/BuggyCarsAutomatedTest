using BuggyCars.AutomatedTest.WebAutomation.Models;
using System.Collections.Generic;

namespace BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers
{
    /// <summary>
    /// Represent the collection of test run context.
    /// </summary>
    public class TestRunContext
    {
        public MemberPersonalDetails Member { get; set; }

        public string Comment { get; set; }

        public int Votes { get; set; }
    }
}
