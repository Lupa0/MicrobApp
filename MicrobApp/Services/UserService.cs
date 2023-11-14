
using MicrobApp.Models;
using System.Text;
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
                    _httpClient.DefaultRequestHeaders.Remove("tenant");
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

        public async void FollowUser(string userNameToFollow)
        {
            string username = SecureStorage.GetAsync("username").Result;
            string apiUrl = $"/Account/FollowUser?&userName={username}&userNameToFollow={userNameToFollow}";

            string tenantId = SecureStorage.GetAsync("tenantId").Result;

            _httpClient.DefaultRequestHeaders.Add("tenant", tenantId);

            string jsonRequest = JsonSerializer.Serialize("");

            HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            await _httpClient.PutAsync(apiUrl, content);
            _httpClient.DefaultRequestHeaders.Remove("tenant");

        }

        public async Task<List<UserProfile>> GetFollowingUsers(string username)
        {
            string apiUrl = $"/Account/GetFollowedUsers?&userName={username}";

            string tenantId = SecureStorage.GetAsync("tenantId").Result;

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
                    _httpClient.DefaultRequestHeaders.Remove("tenant");
                    return JsonSerializer.Deserialize<List<UserProfile>>(response.Content.ReadAsStream(), options);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
            else
            {
                return new List<UserProfile> { };
            }
        }
    }
}
