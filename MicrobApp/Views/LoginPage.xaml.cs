using CommunityToolkit.Maui.Behaviors;

namespace MicrobApp.Views;

public partial class LoginPage : ContentPage
{

    public LoginPage()
    {
        InitializeComponent();

        Loaded += LoginPage_Loaded;
    }

    private async void LoginPage_Loaded(object sender, EventArgs e)
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

        // Realizar la lógica para consumir el endpoint con los datos ingresados
        /*HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync($"tu_endpoint?username={username}&password={password}");

        if (response.IsSuccessStatusCode)
        {
            // La solicitud fue exitosa, maneja la respuesta aquí
        }
        else
        {
            // La solicitud no fue exitosa, maneja los errores aquí
        }*/
    }
}