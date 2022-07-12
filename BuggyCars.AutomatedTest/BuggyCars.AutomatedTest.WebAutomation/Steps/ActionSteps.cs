using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers;
using BuggyCars.AutomatedTest.WebAutomation.Models;
using BuggyCars.AutomatedTest.WebAutomation.Pages;
using BuggyCars.AutomatedTest.WebAutomation.Configuration;

namespace BuggyCars.AutomatedTest.WebAutomation.Steps
{
    /// <summary>
    /// This class represents the collection of action steps.
    /// </summary>
    [Binding]
    public class ActionSteps
    {
        private readonly ExceptionHandler _exceptionHandler;
        private readonly TestData _testData;
        private readonly NavigationHeader _navigationHeader;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionSteps" /> class.
        /// </summary>
        /// <param name="context">The test run context.</param>
        public ActionSteps(ExceptionHandler exceptionHandler, TestData testData, NavigationHeader navigationHeader)
        {
            _exceptionHandler = exceptionHandler;
            _testData = testData;
            _navigationHeader = navigationHeader;
        }

        [When(@"a ""(Basic|Inactive)"" member signs in")]
        public void SignInWithLoyaltyPersona(string loyaltyPersona)
        {
            _exceptionHandler.Execute(() =>
            {
                var member = GetLoyaltyPersona(loyaltyPersona);
                _navigationHeader.TrySignIn(member.Identifier, member.Password);
                //_memberDashboardPage.Member = member;
            });
        }

        [When(@"the member logouts")]
        public void TryLogout()
        {
            _exceptionHandler.Execute(() =>
            {
                _navigationHeader.ClickLogout();
            });
        }
        public MemberPersonalDetails GetLoyaltyPersona(string loyaltyPersona)
        {
            loyaltyPersona = loyaltyPersona.Replace(" ", string.Empty, StringComparison.Ordinal);

            if (!_testData.LoyaltyPersonas.TryGetValue(loyaltyPersona, out var member))
            {
                throw new NotSupportedException($"Persona of type '{loyaltyPersona}' is not supported.");
            }

            return new MemberPersonalDetails
            {
                Identifier = member.Identifier,
                Password = member.Password,
                GivenName = member.Name.GivenName,
                FamilyName = member.Name.FamilyName,
                PhoneNumber = member.PhoneNumber,
            };
        }
    }
}
