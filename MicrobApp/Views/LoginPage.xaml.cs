using Firebase.Auth;
using MicrobApp.Models;
using MicrobApp.Services;
using System.Text.Json;

namespace MicrobApp.Views;

public partial class LoginPage : ContentPage
{

    private readonly AuthenticationService _authenticationService;
    private readonly FirebaseAuthClient _authClient;

    public LoginPage(AuthenticationService authenticationService, FirebaseAuthClient authClient)
    {
        InitializeComponent();
        _authClient = authClient;
        _authenticationService = authenticationService;
        Loaded += LoginPage_Loaded;
    }


    private void LoginPage_Loaded(object sender, EventArgs e)
    {
        _ = StartAnimation();
    }

    private async Task StartAnimation()
    {
        var animations = new List<Task>();
        await stackTop.TranslateTo(0, -75);
        await stackBottom.TranslateTo(0, 75);
        animations.Add(stackParentOfAll.FadeTo(1, 1000, Easing.BounceOut));
        animations.Add(stackTop.TranslateTo(0, 0, 1000));
        animations.Add(stackBottom.TranslateTo(0, 0, 1000));

        await Task.WhenAll(animations);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        string domain = SecureStorage.GetAsync("instanceDomain").Result;
        UsernameEntry.Placeholder = $"Usuario@{domain}";
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        UsernameEntry.Text = null;
        PasswordEntry.Text = null;
    }

    async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Por favor, ingresa tu nombre de usuario y contrase�a.", "OK");
            return;
        }

        UserLogin user = new()
        {
            username = username,
            password = password
        };

        try
        {
            HttpResponseMessage response = await _authenticationService.Login(user);
            Console.WriteLine("Respuesta de la API: " + await response.Content.ReadAsStringAsync());

            UserAuthenticationResponseDto responseBody = JsonSerializer
                .Deserialize<UserAuthenticationResponseDto>(response.Content.ReadAsStream());

            if (response.IsSuccessStatusCode)
            {
                await SecureStorage.SetAsync("token", responseBody.token);
                await SecureStorage.SetAsync("username", user.username);
                await Shell.Current.GoToAsync("//HomePage");
            }
            else
            {
                Console.WriteLine("Error: " + responseBody.errorMessage);
                await DisplayAlert("Error", "Inicio de sesi�n fallido. Por favor, verifica tus credenciales.", "OK");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            await DisplayAlert("Error", "Ha ocurrido un problema. Por favor vuelve a intentar mas tarde.", "OK");
        }
    }

    private async void OnGoogleLoginClicked(object sender, EventArgs e)
    {
        try
        {
            //var result = await _authClient.SignInWithCredentialAsync("accessToken");
            //var firebaseToken = result.FirebaseToken;

            // Puedes usar firebaseToken para realizar acciones adicionales o navegar a la p�gina principal.
            await Shell.Current.GoToAsync("//HomePage");
        }
        catch (FirebaseAuthException ex)
        {
            // Manejar errores de autenticaci�n.
            Console.WriteLine($"Error de autenticaci�n: {ex.Message}");
        }
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        SecureStorage.RemoveAll();
        await Shell.Current.GoToAsync("//InstancePage");
    }
}