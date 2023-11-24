using MicrobApp.Models;
using MicrobApp.Services;

namespace MicrobApp.Views;

public partial class ViewPostPage : ContentPage
{
    private readonly PostService _postService;
    private readonly Post post;
    public ViewPostPage(Post post)
	{
		InitializeComponent();
        this.post = post;
        BindingContext = post;

    }
    private void GoToUserPerfil(object sender, EventArgs e)
    {

    }

    private void Button_Clicked(object sender, EventArgs e)
    {

    }
}