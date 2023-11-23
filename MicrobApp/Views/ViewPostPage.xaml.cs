using MicrobApp.Models;
using MicrobApp.Services;

namespace MicrobApp.Views;

public partial class ViewPostPage : ContentPage
{
    private readonly PostService _postService;
    private readonly string idPost;
    public ViewPostPage(PostService postService, String idPost)
	{
		InitializeComponent();
        _postService = postService;
        this.idPost = idPost;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadViewPost();
    }
    private async void LoadViewPost()
    {
            Post post = await _postService.GetPostById(idPost);
        /*
                List<Comment> commentUsers = await _postService.GetCommentUsers(idPost);
                if (comentUsers.Count > 0)
                {
                   //enviar a view los commentarios
                }
        */
            BindingContext = post;

    }

    private void GoToUserPerfil(object sender, EventArgs e)
    {

    }

    private void Button_Clicked(object sender, EventArgs e)
    {

    }
}