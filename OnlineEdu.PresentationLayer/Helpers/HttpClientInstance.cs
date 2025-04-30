using OnlineEdu.PresentationLayer.Services.TokenServices;
using System.Net.Http.Headers;

namespace OnlineEdu.PresentationLayer.Helpers
{
    public static class HttpClientInstance
    {
        public static HttpClient CreateClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7061/api/");
            return client;
        }
    }
}
