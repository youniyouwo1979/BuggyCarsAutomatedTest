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
        private readonly StringMessages _stringMessages;
        private readonly GeneralElements _generalElements;
        private readonly MemberProfilePage _memberProfilePage;
        private readonly CarPage _carPage;
        private readonly OverallRatingCarsPage _overallRatingCarsPage;
        private readonly TestRunContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionSteps" /> class.
        /// </summary>
        /// <param name="context">The test run context.</param>
        public ValidationSteps(
            ExceptionHandler exceptionHandler,
            TestData testData,
            NavigationHeader navigationHeader,
            StringMessages stringMessages,
            GeneralElements generalElements,
            MemberProfilePage memberProfilePage,
            CarPage carPage,
            OverallRatingCarsPage overallRatingCarsPage,
            TestRunContext context)
        {
            _exceptionHandler = exceptionHandler;
            _testData = testData;
            _navigationHeader = navigationHeader;
            _stringMessages = stringMessages;
            _generalElements = generalElements;
            _memberProfilePage = memberProfilePage;
            _carPage = carPage;
            _overallRatingCarsPage = overallRatingCarsPage;
            _context = context;
        }

        [Then(@"login is successful")]
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

        [Then(@"login fails with the 'invalid credentials' error message")]
        public void VerifyErrorMessageInvalidCredentialsDisplayed()
        {
            _exceptionHandler.Execute(() =>
            {
                _generalElements.VerifyTextDisplayed(_stringMessages.ErrorSignInInvalidCredentials);
            });
        }

        [Then(@"register is successful")]
        public void VerifySignUpSucceeded()
        {
            _exceptionHandler.Execute(() =>
            {
                _generalElements.VerifyTextDisplayed(_stringMessages.SuccessMemberRegistration);
            });
        }

        [Then(@"register fails with the 'already registered loyalty member' error message")]
        public void VerifySignUpFailsWithAlreadyRegisteredLoyaltyMemberErrorMessage()
        {
            _exceptionHandler.Execute(() =>
            {
                _generalElements.VerifyTextDisplayed(_stringMessages.ErrorSignUpLoyaltyAlreadyRegistered);
            });
        }

        [Then(@"update phone number is successful")]
        public void VerifyUpdatePhoneSucceeded()
        {
            _exceptionHandler.Execute(() =>
            {
                _navigationHeader.ClickProfile();
                _memberProfilePage.VerifyPhoneNumber(_context.Member.PhoneNumber);
            });
        }

        [Then(@"the vote is successful")]
        public void VerifyVoteSucceeded()
        {
            _exceptionHandler.Execute(() =>
            {
                _generalElements.VerifyTextDisplayed(_stringMessages.SuccessVoteComplete);
            });
        }

        [Then(@"the comment is added")]
        public void VerifyCommentAdded()
        {
            _exceptionHandler.Execute(() =>
            {
                _generalElements.VerifyTextDisplayed(_context.Comment);
            });
        }

        [Then(@"the vote is increased")]
        public void VerifyVoteIncreased()
        {
            _exceptionHandler.Execute(() =>
            {
                _carPage.VerifyVotesIncreased(_context.Votes);
            });
        }

        [Then(@"all cars are listed with correct ranking")]
        public void VerifyAllCarsListedWithCorrectRanking()
        {
            _exceptionHandler.Execute(() =>
            {
                _overallRatingCarsPage.VerifyRankingForAllCars();
            });
        }

    }
}
