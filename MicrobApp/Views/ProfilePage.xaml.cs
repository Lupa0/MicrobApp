using MicrobApp.Models;

namespace MicrobApp.Views;

public partial class ProfilePage : ContentPage
{
    public UserProfile UserProfile { get; set; }

    public ProfilePage()
    {
        InitializeComponent();
        GetProfileData();
    }

    private void GetProfileData()
    {
        UserProfile = new UserProfile
        {
            ProfileImage = "images/modelprofile.png",
            CoverImage = "timeline.png",
            Name = "Nombre Apellido",
            Username = "@persona@marciano",
            Biography = "Digital Goodies Team - Web u Mobile UI/UX development; Graphics; Illustrations",
            JoinDate = new DateOnly(2021, 4, 1),
            Followers = 10,
            Following = 10,
            IsFollowing = true
        };
        BindingContext = UserProfile;
    }
}