using MicrobApp.Models;
using System.Collections.ObjectModel;
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

            string jsonRequest = JsonSerializer.Serialize(post);

            // Contenido de la solicitud
            HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            // Realiza la solicitud HTTP POST
            return await _httpClient.PostAsync(apiUrl, content);
        }

        public async Task<HttpResponseMessage> CreateComment(int postId, Post post)
        {
            string username = SecureStorage.GetAsync("username").Result;
            string apiUrl = $"/Post/CreateComment?postId={postId}&userName={username}";

            string tenantId = SecureStorage.GetAsync("tenantId").Result;

            _httpClient.DefaultRequestHeaders.Add("tenant", tenantId);

            string jsonRequest = JsonSerializer.Serialize(post);

            // Contenido de la solicitud
            HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            // Realiza la solicitud HTTP POST
            return await _httpClient.PostAsync(apiUrl, content);
        }

        public async Task LikePost(String postId)
        {
         
            string username = SecureStorage.GetAsync("username").Result;
            string apiUrl = $"/Post/AddLikeToPost?postId={postId}&userName={username}";

            Console.WriteLine(apiUrl + "  ckvhzdfhdfhdfghjfdkghdfjgh");

            string tenantId = SecureStorage.GetAsync("tenantId").Result;

            _httpClient.DefaultRequestHeaders.Add("tenant", tenantId);
            
            Post post = new Post();
            post.PostId = int.Parse(postId);

            string jsonRequest = JsonSerializer.Serialize(post);

            // Contenido de la solicitud
            HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            Console.WriteLine(tenantId + "  llegoooooooooh");


            // Realiza la solicitud HTTP POST
            await _httpClient.PostAsync(apiUrl, content);
            Console.WriteLine(tenantId + "  llegoooooooooh finnnnnnnn");

        }

        public async Task<ObservableCollection<Post>> GetPostsByUser(String username, String tenantId)
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
                    _httpClient.DefaultRequestHeaders.Remove("tenant");
                    return JsonSerializer.Deserialize<ObservableCollection<Post>>(response.Content.ReadAsStream(), options);
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

        //no se esta usando, se creo para una funcinalidad y ya no es necesaria. En caso de no reutilizar, hay q borrarla.
        public async Task<Post> GetPostById(String postId)

        {
            string username = SecureStorage.GetAsync("username").Result;
            string tenantId = SecureStorage.GetAsync("tenantId").Result;
            Post resultado = null;

            Task<ObservableCollection<Post>> postsUser = this.GetPostsByUser(username, tenantId);
            ObservableCollection<Post> posts = await postsUser;

            foreach (Post p in posts)
            {
               if( p.PostId.Equals(postId))
                {
                    resultado = p;
                }
            }
            Console.WriteLine(resultado.ToString());
            return resultado;
        }

    }
}
