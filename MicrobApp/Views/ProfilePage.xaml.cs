using MicrobApp.Models;
using MicrobApp.Services;
using System.Collections.ObjectModel;

namespace MicrobApp.Views;

public partial class ProfilePage : ContentPage
{
    private readonly UserService _userService;
    private readonly PostService _postService;
    private readonly string username;
    private readonly string logInAs;

    // Constructor para acceder a través de la TabBar
    public ProfilePage(UserService service, PostService postService)
    {
        InitializeComponent();
        _userService = service;
        _postService = postService;
        username = SecureStorage.GetAsync("username").Result;
        logInAs = username;
        SeguirButton.IsVisible = false;
    }

    public ProfilePage(UserService service, PostService postService, string username)
    {
        InitializeComponent();
        _userService = service;
        _postService = postService;
        this.username = username;
        this.logInAs = SecureStorage.GetAsync("username").Result;
        if (Equals(username, logInAs))
        {
            SeguirButton.IsVisible = false;
        }
        Shell.SetTabBarIsVisible(this, false);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadProfileData();
    }

    private async void LoadProfileData()
    {
        try
        {
            string tenantId = SecureStorage.GetAsync("tenantId").Result;
            Console.WriteLine(tenantId);
            UserProfile user = await _userService.GetUser(username, tenantId);
            user.FollowersNumber = user.Following.Count;
            user.FollowingNumber = user.Followers.Count;

            if (!Equals(username, logInAs))
            {
                List<UserProfile> followingUsers = await _userService.GetFollowingUsers(logInAs);
                if (followingUsers.Count > 0 && followingUsers.Exists(aUser => Equals(aUser.UserName, username)))
                {
                    SeguirButton.Text = "Siguiendo";
                    SeguirButton.IsEnabled = false;
                }
                else
                {
                    SeguirButton.Text = "Seguir";
                }
            }

            BindingContext = user;
            ObservableCollection<Post> posts = await _postService.GetPostsByUser(username, tenantId);
            userPosts.ItemsSource = posts.Reverse();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al obtener los datos del perfil: " + ex.Message);
            await DisplayAlert("Error", "Ha ocurrido un problema. Por favor vuelve a intentar mas tarde.", "OK");
        }
    }

    private async void SeguirButton_Clicked(object sender, EventArgs e)
    {
        if (SeguirButton.Text == "Seguir")
        {
            try
            {
                _userService.FollowUser(username);
                SeguirButton.Text = "Siguiendo";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al seguir usuario: " + username + ". " + ex.Message);
                await DisplayAlert("Error", "Ha ocurrido un problema. Por favor vuelve a intentar mas tarde.", "OK");
            }
        }
    }

    private void GoToUserPerfil(object sender, EventArgs e)
    {
        //Debe ir al perfil del usuario perteneciente a dicho post
        if (!Equals(username, logInAs))
        {
            Navigation.PushAsync(new ProfilePage(new UserService(), new PostService(), username));
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var ListItem = sender as Button;
        String idPost = ListItem.CommandParameter.ToString();
        await _postService.LikePost(idPost);
    }
}