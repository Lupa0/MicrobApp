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

    private void Add_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new PostPage());
    }

    private void GoToUserPerfil(object sender, EventArgs e)
    {
        //Debe ir al perfil del usuario perteneciente a dicho post
        string userName = username; //Username del usuario del post seleccionado
        Navigation.PushAsync(new ProfilePage(new UserService(), new PostService(), userName));
    }

    private void Redirect_to_settings(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SettingsPage());
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var ListItem = sender as Button;
        ListItem.BackgroundColor = Color.FromRgb(234,92,85);
        ListItem.BorderColor = Color.FromRgb(255, 255, 255);
        String idPost = ListItem.CommandParameter.ToString();
        await _postService.LikePost(idPost);
    }

    private void OnLabelTapped(object sender, TappedEventArgs e)
    {
        //var ListItem = sender as TapGestureRecognizer;
        //String idPost = ListItem.CommandParameter.ToString();
        Label label = (Label)sender;

        // Obtén el BindingContext de la Label, que debe ser un objeto Post
        Post post = (Post)label.BindingContext;

        //Post seleccionado
        Navigation.PushAsync(new ViewPostPage(post));
    }
}