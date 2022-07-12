namespace BuggyCars.AutomatedTest.WebAutomation.Models
{
    public class LoyaltyPersona
    {
        public string Identifier { get; set; }

        public string Password { get; set; }

        public PersonName Name { get; set; } = new PersonName();

        public string PhoneNumber { get; set; }
    }
}
