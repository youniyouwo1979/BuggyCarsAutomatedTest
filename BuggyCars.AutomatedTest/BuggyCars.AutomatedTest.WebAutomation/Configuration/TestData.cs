using System.Collections.Generic;
using BuggyCars.AutomatedTest.WebAutomation.Models;

namespace BuggyCars.AutomatedTest.WebAutomation.Configuration
{
    public class TestData
    {
        public Dictionary<string, LoyaltyPersona> LoyaltyPersonas { get; set; }

        public string MemberIdentifierUnregistered { get; set; }

        public string MemberIdentifierUnregisteredPassword { get; set; }

        public SignUpDetails SignUpDetails { get; set; }

        public string Comment { get; set; }
    }
}
