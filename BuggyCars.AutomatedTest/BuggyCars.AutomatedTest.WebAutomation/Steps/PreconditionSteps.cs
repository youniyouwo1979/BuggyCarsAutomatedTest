using BuggyCars.AutomatedTest.WebAutomation.AuxiliaryMethods.Helpers;
using BuggyCars.AutomatedTest.WebAutomation.Pages;
using TechTalk.SpecFlow;

namespace BuggyCars.AutomatedTest.WebAutomation.Steps
{
    /// <summary>
    /// This class represents the collection of precondition steps.
    /// </summary>
    [Binding]
    public class PreconditionSteps
    {
        private readonly TestRunContext _context;
        private readonly ExceptionHandler _exceptionHandler;
        private readonly HomePage _homePage;
        private readonly ActionSteps _actionSteps;
        private readonly ValidationSteps _validationSteps;
        private readonly NavigationHeader _navigationHeader;

        /// <summary>
        /// Initializes a new instance of the <see cref="PreconditionSteps" /> class.
        /// </summary>
        /// <param name="context">The test run context.</param>
        public PreconditionSteps(TestRunContext context, HomePage homePage, ExceptionHandler exceptionHandler, ActionSteps actionSteps, NavigationHeader navigationHeader, ValidationSteps validationSteps)
        {
            _context = context;
            _homePage = homePage;
            _exceptionHandler = exceptionHandler;
            _actionSteps = actionSteps;
            _validationSteps = validationSteps;
            _navigationHeader = navigationHeader;
        }

        /// <summary>
        /// The precondition step to navigate to home page.
        /// </summary>
        [Given(@"a home page")]
        public void NavigateToHomePage()
        {
            _exceptionHandler.Execute(() =>
            {
                _homePage.NavigateToHomePage();
            });
        }

        [Given(@"a ""(Basic|Inactive)"" member")]
        public void SignInSucceededWithLoyaltyPersona(string loyaltyPersona)
        {
            _exceptionHandler.Execute(() =>
            {
                _actionSteps.SignInWithLoyaltyPersona(loyaltyPersona);
                _validationSteps.VerifySignInSucceeded();
            });
        }
    }
}
