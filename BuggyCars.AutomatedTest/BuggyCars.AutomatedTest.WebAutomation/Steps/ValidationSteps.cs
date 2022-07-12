using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers;
using BuggyCars.AutomatedTest.WebAutomation.Pages;
using BuggyCars.AutomatedTest.WebAutomation.Configuration;

namespace BuggyCars.AutomatedTest.WebAutomation.Steps
{
    /// <summary>
    /// This class represents the collection of validation steps.
    /// </summary>
    [Binding]
    public class ValidationSteps
    {
        private readonly ExceptionHandler _exceptionHandler;
        private readonly TestData _testData;
        private readonly NavigationHeader _navigationHeader;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionSteps" /> class.
        /// </summary>
        /// <param name="context">The test run context.</param>
        public ValidationSteps(ExceptionHandler exceptionHandler, TestData testData, NavigationHeader navigationHeader)
        {
            _exceptionHandler = exceptionHandler;
            _testData = testData;
            _navigationHeader = navigationHeader;
        }

        [Then(@"sign in is successful")]
        public void VerifySignInSucceeded()
        {
            _exceptionHandler.Execute(() =>
            {
                _navigationHeader.VerifyLogoutDisplayed();
            });
        }

        [Then(@"logout is successful")]
        public void VerifyLogoutSucceeded()
        {
            _exceptionHandler.Execute(() =>
            {
                _navigationHeader.VerifyLoginButtonDisplayed();
            });
        }
    }
}
