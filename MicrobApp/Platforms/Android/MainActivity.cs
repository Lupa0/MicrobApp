using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Auth.Api.SignIn;

namespace MicrobApp;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    // Event 
    public static event EventHandler<(bool Success, GoogleSignInAccount Account)> ResultGoogleAuth;

    // Result of auth activity
    protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
    {
        base.OnActivityResult(requestCode, resultCode, data);
        if (requestCode == 1)
        {
            try
            {
                if (data == null)
                {
                    Console.WriteLine("Esta vacio");
                }
                var currentAccount = await GoogleSignIn.GetSignedInAccountFromIntentAsync(data);
                if (currentAccount != null)
                {
                    Console.WriteLine("Exito");
                    ResultGoogleAuth?.Invoke(this, (currentAccount.Email != null, currentAccount));
                }
                else
                {
                    Console.WriteLine("Fallo");
                    ResultGoogleAuth?.Invoke(this, (false, null));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
}