using System;
using TechTalk.SpecFlow;
using BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers;
using BuggyCars.AutomatedTest.WebAutomation.Models;
using BuggyCars.AutomatedTest.WebAutomation.Pages;
using BuggyCars.AutomatedTest.WebAutomation.Configuration;
using System.Globalization;

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
        private readonly SignUpPage _signUpPage;
        private readonly MemberProfilePage _memberProfilePage;
        private readonly TestRunContext _context;
        private readonly GeneralElements _generalElements;
        private readonly StringMessages _stringMessages;
        private readonly ValidationSteps _validationSteps;
        private readonly HomePage _homePage;
        private readonly CarPage _carPage;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionSteps" /> class.
        /// </summary>
        /// <param name="context">The test run context.</param>
        public ActionSteps(
            ExceptionHandler exceptionHandler, 
            TestData testData, 
            NavigationHeader navigationHeader, 
            SignUpPage signUpPage, 
            MemberProfilePage memberProfilePage, 
            TestRunContext context, 
            GeneralElements generalElements,
            StringMessages stringMessages,
            ValidationSteps validationSteps,
            HomePage homePage,
            CarPage carPage)
        {
            _exceptionHandler = exceptionHandler;
            _testData = testData;
            _navigationHeader = navigationHeader;
            _signUpPage = signUpPage;
            _memberProfilePage = memberProfilePage;
            _context = context;
            _generalElements = generalElements;
            _stringMessages = stringMessages;
            _validationSteps = validationSteps;
            _homePage = homePage;
            _carPage = carPage;
        }

        [When(@"a ""(Basic|Inactive)"" member logins")]
        public void SignInWithLoyaltyPersona(string loyaltyPersona)
        {
            _exceptionHandler.Execute(() =>
            {
                var member = GetLoyaltyPersona(loyaltyPersona);
                _navigationHeader.TrySignIn(member.Login, member.Password);
                _context.Member = member;
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

        [When(@"an attempt is made to login with an unregistered member")]
        public void AttemptSignInWithoutMemberIdentifier()
        {
            _exceptionHandler.Execute(() =>
            {
                _navigationHeader.TrySignIn(_testData.MemberIdentifierUnregistered, _testData.MemberIdentifierUnregisteredPassword);
            });
        }

        [When(@"a new member registers")]
        public void SignUpWithNewMember()
        {
            _exceptionHandler.Execute(() =>
            {
                _navigationHeader.ClickRegister();
                var signUpDetails = GetSignUpDetail();
                _signUpPage.TrySignUp(signUpDetails);
                var member = new MemberPersonalDetails
                {
                    FirstName = signUpDetails.Name.FirstName,
                    LastName = signUpDetails.Name.LastName,
                    Password = signUpDetails.Password,
                    Login = signUpDetails.Login,
                };
                _context.Member = member;
            });
        }

        [When(@"an attempt is made to register with an existing loyalty member")]
        public void AttemptSignUpWithExistingLoyaltyMember()
        {
            _exceptionHandler.Execute(() =>
            {
                _navigationHeader.ClickRegister();
                var member = GetLoyaltyPersona(LoyaltyPersonas.Basic);
                var signUpDetails = GetSignUpDetail();
                signUpDetails.Login = member.Login;
                _signUpPage.TrySignUp(signUpDetails);
            });
        }

        [When(@"the member phone number is updated")]
        public void UpdateMemberPhoneNumber()
        {
            _exceptionHandler.Execute(() =>
            {
                _navigationHeader.ClickProfile();
                var memberPersonalDetails = new MemberPersonalDetails
                {
                    PhoneNumber = GenerateRamdomNumber().ToString(CultureInfo.InvariantCulture)
                };
                _memberProfilePage.TryUpdateMemberDetails(memberPersonalDetails);                
                _generalElements.VerifyTextDisplayed(_stringMessages.SuccessMemberUpdateProfile);
                _context.Member.PhoneNumber = memberPersonalDetails.PhoneNumber;
            });
        }

        [When(@"the member re-logins")]
        public void ReLogins()
        {
            _exceptionHandler.Execute(() =>
            {

                var member = _context.Member;
                _navigationHeader.TrySignIn(member.Login, member.Password);
            });
        }

        [When(@"a new member votes the most popular car with a comment")]
        public void VotesMostPopularCarWithAComment()
        {
            _exceptionHandler.Execute(() =>
            {
                SignUpWithNewMember();
                _validationSteps.VerifySignUpSucceeded();
                _navigationHeader.TrySignIn(_context.Member.Login, _context.Member.Password);
                _navigationHeader.ClickLogo();
                _homePage.SelectPopularModel();
                _context.Votes = _carPage.GetTotalVotes();
                var comment = _testData.Comment + GenerateRamdomNumber();
                _carPage.VoteWithComment(comment);
                _generalElements.VerifyTextDisplayed(_stringMessages.SuccessVoteComplete);
                _context.Comment = comment;
            });
        }

        [When(@"overal rating of all cars is checked")]
        public void CheckOveralRatingOfAllCars()
        {
            _exceptionHandler.Execute(() =>
            {
                _homePage.SelectOverallRating();
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
                Login = member.Login,
                Password = member.Password,
                FirstName = member.Name.FirstName,
                LastName = member.Name.LastName,
                PhoneNumber = member.PhoneNumber,
            };
        }

        private SignUpDetails GetSignUpDetail()
        {
            var signUpDetails = new SignUpDetails
            {
                Name = _testData.SignUpDetails.Name,
                Login = _testData.SignUpDetails.Login + GenerateRamdomNumber(),
                Password = _testData.SignUpDetails.Password
            };

            return signUpDetails;
        }

        private int GenerateRamdomNumber()
        {
            Random random = new Random();
            return random.Next(000000000, 999999999);
        }
    }
}
