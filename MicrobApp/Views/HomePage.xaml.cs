using MicrobApp.Models;
using MicrobApp.Services;
using System.Collections.ObjectModel;

namespace MicrobApp.Views;

public partial class HomePage : ContentPage
{
    private readonly PostService _postService;

    private ObservableCollection<Post> posts = new();
    private readonly string tenantId;
    private readonly string username;

    public HomePage()
    {
        InitializeComponent();
        _postService = new PostService();
        tenantId = SecureStorage.GetAsync("tenantId").Result;
        username = SecureStorage.GetAsync("username").Result;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadPosts();
    }
    private async void LoadPosts()
    {
        postListView.ItemsSource = null;
        posts.Clear();
        try
        {
            posts = await _postService.GetPostsByUser(username, tenantId);
            postListView.ItemsSource = posts.Reverse();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al obtener los posts: " + ex.Message);
            await DisplayAlert("Error", "Ha ocurrido un problema. Por favor vuelve a intentar mas tarde.", "OK");
        }
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
        Navigation.PushAsync(new ProfilePage(new UserService(), new PostService(), username));
    }

    private void Redirect_to_settings(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SettingsPage());
    }
}