using MicrobApp.Models;
using MicrobApp.Services;

namespace MicrobApp.Views;

public partial class ViewPostPage : ContentPage
{
    public ViewPostPage(Post post)
    {
        InitializeComponent();
        BindingContext = post;

    }
    private void GoToUserPerfil(object sender, EventArgs e)
    {

    }

    private void Button_Clicked(object sender, EventArgs e)
    {

    }
}