
using MicrobApp.Models;
using System.Text.Json;

namespace MicrobApp.Services
{
    public class UserService
    {
        private HttpClient _httpClient;
        public UserService()
        {
            _httpClient = HttpClientFactory.CreateHttpClient();
        }

        public async Task<UserProfile> GetUser(String username, String tenantId)
        {
            string apiUrl = $"/Account/GetUser?userName={username}";
            _httpClient.DefaultRequestHeaders.Add("tenant", tenantId);

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    Console.WriteLine("Respuesta de la API: " + await response.Content.ReadAsStringAsync());
                    var options = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };
                    return JsonSerializer.Deserialize<UserProfile>(response.Content.ReadAsStream(), options);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
            else
            {
                Console.WriteLine("Response status: " + response.StatusCode);
                throw new Exception("Ha ocurrido un problema");
            }
        }
    }
}
