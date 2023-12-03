using MicrobApp.Models;
using System.Text.Json;

namespace MicrobApp.Services
{
    public class InstanceService
    {
        private HttpClient _httpClient;
        public InstanceService()
        {
            _httpClient = HttpClientFactory.CreateHttpClient();
        }

        public async Task<Instance> GetInstanceByDomain()
        {
            string domain = SecureStorage.GetAsync("instanceDomain").Result;
            string apiUrl = $"/Instance/GetInstanceByDomain?domain={domain}";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            Console.WriteLine("Respuesta de la API: " + await response.Content.ReadAsStringAsync());

            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                return JsonSerializer.Deserialize<Instance>(response.Content.ReadAsStream(), options);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(apiUrl + ": " + ex.StackTrace);
            }
            
        }


        public async Task<List<Instance>> GetActiveInstance()
        {
            string apiUrl = "/Instance/GetActiveInstances?Page=1&ItemsperPage=50";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            Console.WriteLine("Respuesta de la API: " + await response.Content.ReadAsStringAsync());

            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                return JsonSerializer.Deserialize<List<Instance>>(response.Content.ReadAsStream(), options);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(apiUrl + ": " + ex.StackTrace);
            }

        }

    }
}
