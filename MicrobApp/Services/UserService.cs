
using MicrobApp.Models;
using System.Collections.ObjectModel;
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
            string apiUrl = $"/Account/GetFollowedUsers?Page=1&ItemsPerPage=50&userName={username}";

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

        public async Task<PostPaginated> GetUserTimeline(int numberPage, int pageSize)
        {
            string username = SecureStorage.GetAsync("username").Result;

            string apiUrl = $"/Account/GetUserTimeline?Page={numberPage}&ItemsPerPage={pageSize}&userName={username}";
            Console.WriteLine(apiUrl);
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
                    PostPaginated postPaginated = new()
                    {
                        Posts = JsonSerializer.Deserialize<ObservableCollection<Post>>(response.Content.ReadAsStream(), options)
                    };

                    try
                    {
                        postPaginated = ExtractXPaginationHeader(response, options, postPaginated);
                    }
                    catch
                    {
                        Console.WriteLine("Error here");
                    }
                    return postPaginated;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
            else
            {
                return new PostPaginated();
            }
        }

        private PostPaginated ExtractXPaginationHeader(HttpResponseMessage response, JsonSerializerOptions options, PostPaginated postPaginated)
        {
            // Obtener valores del encabezado X-Pagination
            if (response.Headers.TryGetValues("X-Pagination", out IEnumerable<string> paginationValues))
            {
                var paginationJson = paginationValues.FirstOrDefault();

                if (!string.IsNullOrEmpty(paginationJson))
                {
                    var paginationData = JsonSerializer.Deserialize<Dictionary<string, object>>(paginationJson, options);

                    // Asignar los valores al objeto PostPaginated
                    if (paginationData.ContainsKey("CurrentPage"))
                    {
                        postPaginated.CurrentPage = int.Parse(paginationData["CurrentPage"].ToString());
                    }

                    if (paginationData.ContainsKey("TotalCount"))
                    {
                        postPaginated.TotalCount = int.Parse(paginationData["TotalCount"].ToString());
                    }

                    if (paginationData.ContainsKey("TotalPages"))
                    {
                        postPaginated.TotalPages = int.Parse(paginationData["TotalPages"].ToString());
                    }

                    if (paginationData.ContainsKey("HasPrevious"))
                    {
                        postPaginated.HasPrevious = bool.Parse(paginationData["HasPrevious"].ToString());
                    }

                    if (paginationData.ContainsKey("HasNext"))
                    {
                        postPaginated.HasNext = bool.Parse(paginationData["HasNext"].ToString());
                    }
                }
            }
            return postPaginated;
        }
    }
}
