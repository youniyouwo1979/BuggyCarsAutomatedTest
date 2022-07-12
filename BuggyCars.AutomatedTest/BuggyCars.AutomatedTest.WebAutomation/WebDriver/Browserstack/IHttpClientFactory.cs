using System.Net.Http;

namespace BuggyCars.AutomatedTest.WebAutomation.WebDriver.Browserstack
{
    public interface IHttpClientFactory
    {
        HttpClient GetHttpClient();
    }
}
