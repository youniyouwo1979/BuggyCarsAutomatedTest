namespace BuggyCars.AutomatedTest.WebAutomation.Models
{
    public class LoyaltyPersona
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public PersonName Name { get; set; } = new PersonName();

        public string PhoneNumber { get; set; }
    }
}
