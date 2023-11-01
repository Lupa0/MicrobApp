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

        public async Task<Instance> GetInstanceByDomain(String domain)
        {
            string apiUrl = $"Instance/GetInstanceByDomain?domain={domain}";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            try
            {
                return JsonSerializer.Deserialize<Instance>(await response.Content.ReadAsStringAsync());
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(apiUrl + ": " + ex.StackTrace);
            }
            
        }
    }
}
