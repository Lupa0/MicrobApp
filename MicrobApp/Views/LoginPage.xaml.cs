using MicrobApp.Models;
using MicrobApp.Services;
using System.Text.Json;

namespace MicrobApp.Views;

public partial class LoginPage : ContentPage
{

    private readonly AuthenticationService _authenticationService;
    private readonly InstanceService _instanceService;

    public LoginPage(AuthenticationService authenticationService, InstanceService instanceService)
    {
        InitializeComponent();
        _authenticationService = authenticationService;
        _instanceService = instanceService;
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

    async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Por favor, ingresa tu nombre de usuario y contraseña.", "OK");
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
                int atIndex = user.username.IndexOf('@');

                if (atIndex >= 0 && atIndex < user.username.Length - 1)
                {
                    string domain = user.username.Substring(atIndex + 1);
                    Console.WriteLine(domain);
                    await SecureStorage.SetAsync("instanceDomain", domain);
                    Instance instance = await _instanceService.GetInstanceByDomain(domain);
                    Console.WriteLine("instancia: " + instance.TenantInstanceId);
                }

                await SecureStorage.SetAsync("token", responseBody.token);
                await SecureStorage.SetAsync("username", user.username);
                await Shell.Current.GoToAsync("//HomePage");
            }
            else
            {
                Console.WriteLine("Error: " + responseBody.errorMessage);
                await DisplayAlert("Error", "Inicio de sesión fallido. Por favor, verifica tus credenciales.", "OK");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            await DisplayAlert("Error", "Ha ocurrido un problema. Por favor vuelve a intentar mas tarde.", "OK");
        }
    }
}