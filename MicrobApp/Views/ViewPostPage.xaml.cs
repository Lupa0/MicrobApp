using MicrobApp.Models;
using MicrobApp.Services;

namespace MicrobApp.Views;

public partial class ViewPostPage : ContentPage
{
    private Post _post = new Post();
    private readonly PostService _postService;

    private ICollection<Post> comments = new List<Post>();
    public ViewPostPage(Post post)
    {
        InitializeComponent();
        _postService = new PostService();
        _post = post;
        BindingContext = post;
    }
    private void GoToUserPerfil(object sender, EventArgs e)
    {
        var ListItem = sender as ImageButton;
        string userName = ListItem.CommandParameter.ToString(); //Username del usuario del post seleccionado
        Navigation.PushAsync(new ProfilePage(new UserService(), new PostService(), userName));
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadPosts();
    }

    private async void LoadPosts()
    {
        commentListView.ItemsSource = null;
        this.comments.Clear();
        try
        {
            
            this.comments = _post.Comments;
            commentListView.ItemsSource = comments.Reverse();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al obtener los posts: " + ex.Message);
            await DisplayAlert("Error", "Ha ocurrido un problema. Por favor vuelve a intentar mas tarde.", "OK");
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var ListItem = sender as Button;
        ListItem.BackgroundColor = Color.FromRgb(234, 92, 85);
        ListItem.BorderColor = Color.FromRgb(255, 255, 255);
        String idPost = ListItem.CommandParameter.ToString();
        await _postService.LikePost(idPost);
    }
    private void AnswerPost(object sender, EventArgs e)
    {
        var ListItem = sender as Button;
        string inResponseTo = ListItem.CommandParameter.ToString();
        Navigation.PushAsync(new PostPage(inResponseTo, true));
    }

    private async void Reportar_Post(object sender, EventArgs e)
    {
        await _postService.ReportPost(_post.PostId);
        await DisplayAlert("Hecho", "Este post ha sido reportado", "OK");
        await Shell.Current.GoToAsync("..");
    }
}