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

        public async Task<HttpResponseMessage> Login()
        {
            string apiUrl = "/Account/Login";

            // Datos del usuario para la solicitud
            var user = new UserLogin
            {
                email = "cuchosmarket@gmail.com",
                password = "Pass_1234"
            };

            // Serializa el objeto UserLogin a JSON
            string jsonRequest = JsonSerializer.Serialize(user);

            // Establece el encabezado tenant y el tipo de contenido de la solicitud
            _httpClient.DefaultRequestHeaders.Add("tenant", "0");

            // Contenido de la solicitud
            HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            // Realiza la solicitud HTTP POST
            return await _httpClient.PostAsync(apiUrl, content);
        }
    }
}
