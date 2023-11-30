using Android.App;
using Android.Gms.Auth.Api.SignIn;
using MicrobApp.Models;
using MicrobApp.Services;

namespace MicrobApp
{
    public partial class GoogleAuthService : IGoogleAuthService
    {
        private Activity _activity;
        private GoogleSignInOptions _gso;
        private GoogleSignInClient _googleSignInClient;
        private TaskCompletionSource<GoogleSignInResponseDTO> _taskCompletionSource;
        private Task<GoogleSignInResponseDTO> GoogleAuthentication
        {
            get => _taskCompletionSource.Task;
        }

        // Android platform
        public GoogleAuthService()
        {
            // Get current activity
            _activity = Platform.CurrentActivity;

            // Config Auth Option
            _gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                            .RequestIdToken("996737835227-h0kr9hg7uq313p6g9iv14oa4vsk429ns.apps.googleusercontent.com")
                            .RequestEmail()
                            .RequestId()
                            .Build();

            // Get client
            _googleSignInClient = GoogleSignIn.GetClient(_activity, _gso);
            MainActivity.ResultGoogleAuth += MainActivity_ResultGoogleAuth;

        }

        public Task<GoogleSignInResponseDTO> AuthenticateAsync()
        {
            _taskCompletionSource = new TaskCompletionSource<GoogleSignInResponseDTO>();

            _activity.StartActivityForResult(_googleSignInClient.SignInIntent, 1);

            return _taskCompletionSource.Task; 
        }

        private void MainActivity_ResultGoogleAuth(object sender, (bool Success, GoogleSignInAccount Account) e)
        {
            Console.WriteLine(e.Success);
            Console.WriteLine(e.Account);
            if (e.Success && e.Account != null)
            {
                _taskCompletionSource.SetResult(new GoogleSignInResponseDTO
                {
                    Success = true,
                    IdToken = e.Account.IdToken
                });
            }
            else
            {
                _taskCompletionSource.SetResult(new GoogleSignInResponseDTO
                {
                    Success = false,
                    IdToken = null // or set to a default value
                });
            }
        }


        public Task LogoutAsync() => _googleSignInClient.SignOutAsync();

    }
}
