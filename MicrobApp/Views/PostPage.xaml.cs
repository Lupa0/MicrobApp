using MicrobApp.Models;
using MicrobApp.Services;

namespace MicrobApp.Views;

public partial class PostPage : ContentPage
{
    private readonly PostService _postService;
    private readonly string respondsTo;
    public PostPage()
    {
        InitializeComponent();
        _postService = new PostService();
    }

    public PostPage(string idPost)
    {
        InitializeComponent();
        _postService = new PostService();
        respondsTo = idPost;
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
            await Shell.Current.GoToAsync("//HomePage");

        }


    }
}