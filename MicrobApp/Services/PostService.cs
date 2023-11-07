using MicrobApp.Models;
using System.Text;
using System.Text.Json;

namespace MicrobApp.Services
{
    public class PostService
    {
        private HttpClient _httpClient;

        public PostService() 
        {
            _httpClient = HttpClientFactory.CreateHttpClient();
        }

        public async Task<HttpResponseMessage> DoPost(Post post)
        {
            string username = SecureStorage.GetAsync("username").Result;
            string apiUrl = $"/Post/CreatePost?userName={username}";

            string tenantId = SecureStorage.GetAsync("tenantId").Result;

            _httpClient.DefaultRequestHeaders.Add("tenant", tenantId);

            // Serializa el objeto UserLogin a JSON
            string jsonRequest = JsonSerializer.Serialize(post);

            // Contenido de la solicitud
            HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            // Realiza la solicitud HTTP POST
            return await _httpClient.PostAsync(apiUrl, content);
        }

        public async Task<List<Post>> GetPostsByUser(String username, String tenantId)
        {
            string apiUrl = $"/Post/GetPostByUser?userName={username}";
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
                    return JsonSerializer.Deserialize<List<Post>>(response.Content.ReadAsStream(), options);
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
