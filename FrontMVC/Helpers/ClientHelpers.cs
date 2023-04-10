using FrontMVC.Services;
using System.Net.Http.Headers;

namespace FrontMVC.Helpers
{
    public class ClientHelpers
    {
        private TokenService tokenService;

        public ClientHelpers(TokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        public HttpClient gerarClienComToken(string uri)
        {
            HttpClient client = new HttpClient();
            string token = tokenService.GetToken();
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;

        }
    }
}
