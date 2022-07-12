using System.Collections.Generic;
using BuggyCars.AutomatedTest.WebAutomation.Models;

namespace BuggyCars.AutomatedTest.WebAutomation.Configuration
{
    public class TestData
    {
        public Dictionary<string, LoyaltyPersona> LoyaltyPersonas { get; set; }

        public string MemberIdentifierUnregistered { get; set; }

        public string MemberIdentifierUnregisteredPassword { get; set; }

        public string MemberIncorrectPassword { get; set; }

        public string PasswordWithoutUppercase { get; set; }

        public SignUpDetails SignUpDetails { get; set; }
    }
}
