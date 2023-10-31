using MicrobApp.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace MicrobApp.Services
{
    public class AuthenticationService
    {
        private HttpClient _httpClient;
        public AuthenticationService()
        {
            _httpClient = HttpClientFactory.CreateHttpClient();
        }

        public async Task<HttpResponseMessage> Login(UserLogin user)
        {
            string apiUrl = "/Account/Login";

            // Serializa el objeto UserLogin a JSON
            string jsonRequest = JsonSerializer.Serialize(user);

            // Contenido de la solicitud
            HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            // Realiza la solicitud HTTP POST
            return await _httpClient.PostAsync(apiUrl, content);
        }
    }
}
