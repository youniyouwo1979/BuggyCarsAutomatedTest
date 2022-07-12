using System.Net.Http;

namespace BuggyCars.AutomatedTest.WebAutomation.WebDriver.Browserstack
{
    public class HttpClientFactory : IHttpClientFactory
    {
        private readonly HttpClient _httpClient;

        public HttpClientFactory(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public HttpClient GetHttpClient()
        {
            return _httpClient;
        }
    }
}
