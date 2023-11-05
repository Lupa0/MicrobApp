using MicrobApp.Models;
using MicrobApp.Services;
using System.Text.Json;

namespace MicrobApp.Views;

public partial class HomePage : ContentPage
{
    private readonly InstanceService _instanceService;

    public HomePage(InstanceService instanceService)
    {
        InitializeComponent();
        _instanceService = instanceService;
        LoadInstanceData();
    }

    private async void LoadInstanceData()
    {
        try
        {
            string domain = SecureStorage.GetAsync("instanceDomain").Result;
            Instance instance = await _instanceService.GetInstanceByDomain(domain);
            await SecureStorage.SetAsync("tenantId", instance.TenantInstanceId.ToString());
            Console.WriteLine("instancia: " + instance.TenantInstanceId);

        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al obtener los datos de la instancia: " + ex.Message);
            await DisplayAlert("Error", "Ha ocurrido un problema. Por favor vuelve a intentar mas tarde.", "OK");
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        string fileName;
        List<Post> posts = new List<Post>();


        string folderPath = FileSystem.AppDataDirectory;
        fileName = Path.Combine(folderPath, "post.json");
        if (File.Exists(fileName))
        {
            string jsonData = File.ReadAllText(fileName);

            posts = JsonSerializer.Deserialize<List<Post>>(jsonData);
        }

        // Configura la ListView para mostrar los posts
        postListView.ItemsSource = posts;

        //LoadInstanceData();
    }

    private void OnImageButtonClicked(object sender, EventArgs e)
    {
        // Navegar a la segunda página (SecondPage)
        Navigation.PushAsync(new PostPage());
    }

    private void Add_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new PostPage());
    }

    private void GoToUserPerfil(object sender, EventArgs e)
    {
        //Debe ir al perfil del usuario perteneciente a dicho post
        string username = ""; //Username del usuario del post seleccionado
        Navigation.PushAsync(new ProfilePage(new Services.UserService(), username));
    }

    private void Redirect_to_settings(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SettingsPage());
    }
}