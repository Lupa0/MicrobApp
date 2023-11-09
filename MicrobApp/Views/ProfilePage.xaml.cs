using MicrobApp.Models;
using MicrobApp.Services;

namespace MicrobApp.Views;

public partial class ProfilePage : ContentPage
{
    private readonly UserService _userService;
    private readonly string username;

    // Constructor para acceder a través de la TabBar
    public ProfilePage(UserService service)
    {
        InitializeComponent();
        _userService = service;
        username = SecureStorage.GetAsync("username").Result;
        LoadProfileData();
        SeguirButton.IsVisible = false;
    }

    public ProfilePage(UserService service, string username)
    {
        InitializeComponent();
        _userService = service;
        this.username = username;
        if (!Equals(username, SecureStorage.GetAsync("username").Result))
        {
            SeguirButton.IsVisible = true;
        }
        LoadProfileData();
        Shell.SetTabBarIsVisible(this, false);
    }

    private async void LoadProfileData()
    {
        try
        {
            string tenantId = SecureStorage.GetAsync("tenantId").Result;
            Console.WriteLine(tenantId);
            UserProfile user = await _userService.GetUser(username, tenantId);
            user.Following = user.FollowUsers.Count;

            if (user.IsFollowing)
            {
                SeguirButton.Text = "Siguiendo";
            } else
            {
                SeguirButton.Text = "Seguir";
            }

            BindingContext = user;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al obtener los datos del perfil: " + ex.Message);
            await DisplayAlert("Error", "Ha ocurrido un problema. Por favor vuelve a intentar mas tarde.", "OK");
        }
    }
}