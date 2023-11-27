using MicrobApp.Models;
using MicrobApp.Services;

namespace MicrobApp.Views;

public partial class PostPage : ContentPage
{
    private readonly PostService _postService;
    private readonly string respondsTo;
    private bool accessFromProfilePage = false;
    public string TitleText { get; private set; }

    public PostPage()
    {
        InitializeComponent();
        _postService = new PostService();
        TitleText = "Nuevo post";
        BindingContext = this;
    }

    public PostPage(string idPost, bool fromProfilePage)
    {
        InitializeComponent();
        _postService = new PostService();
        respondsTo = idPost;
        accessFromProfilePage = fromProfilePage;
        TitleText = "Responder a post";
        BindingContext = this;
    }

    private async void Publicar(object sender, EventArgs e)
    {
        string message = TextEditor.Text;

        if (message != "")
        {
            Post post = new()
            {
                Text = message
            };

            try
            {
                if (respondsTo == null)
                {
                    await _postService.DoPost(post);
                } else
                {
                    await _postService.CreateComment(respondsTo, post);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los datos de la instancia: " + ex.Message);
                await DisplayAlert("Error", "Ha ocurrido un problema. Por favor vuelve a intentar mas tarde.", "OK");
                await Shell.Current.GoToAsync("..");
            }
            
            if (accessFromProfilePage)
            {
                await Shell.Current.GoToAsync("..");
            } else
            {
                await Shell.Current.GoToAsync("//HomePage");
            }

        }


    }
}