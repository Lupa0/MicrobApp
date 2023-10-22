
namespace MicrobApp.Models
{
    public class UserAuthenticationResponseDto
    {
        public bool isAuthSuccessful { get; set; }
        public string? errorMessage { get; set; }
        public string? token { get; set; }
    }
}
