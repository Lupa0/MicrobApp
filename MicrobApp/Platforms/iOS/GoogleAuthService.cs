using MicrobApp.Models;
using MicrobApp.Services;

namespace MicrobApp
{
    public partial class GoogleAuthService : IGoogleAuthService
    {
        public Task<GoogleSignInResponseDTO> AuthenticateAsync()
        {
            throw new NotImplementedException();
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
