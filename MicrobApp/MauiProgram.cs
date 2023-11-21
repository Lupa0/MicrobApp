using CommunityToolkit.Maui;
using Firebase.Auth;
using Firebase.Auth.Providers;
using MicrobApp.Services;
using MicrobApp.Views;

namespace MicrobApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

            });

        //Inyeccion de dependencias
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<ProfilePage>();
        builder.Services.AddTransient<PostService>();


        //Se agregan servicios
        builder.Services.AddSingleton<AuthenticationService>();
        builder.Services.AddSingleton<UserService>();
        builder.Services.AddSingleton<InstanceService>();
        builder.Services.AddSingleton<PostService>();

        builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig()
        {
            ApiKey = "AIzaSyAV-RqsJm1tPx3AiLKHNcZqMtOW8Q4KHQg",
            AuthDomain = "laboratorionet-e6d3f.firebaseapp.com",
            Providers = new FirebaseAuthProvider[]
            {
                new GoogleProvider().AddScopes("email")
            }
        }));

        return builder.Build();
    }
}
