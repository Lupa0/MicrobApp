using MicrobApp.Models;

namespace MicrobApp.Services
{
    public interface IGoogleAuthService
    {
        Task<GoogleSignInResponseDTO> AuthenticateAsync();
        Task LogoutAsync();
    }

}
