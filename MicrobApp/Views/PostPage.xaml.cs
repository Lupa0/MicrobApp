using MicrobApp.Models;
using MicrobApp.Services;

namespace MicrobApp.Views;

public partial class PostPage : ContentPage
{
    private readonly PostService _postService;
    public PostPage()
    {
        InitializeComponent();
        _postService = new PostService();
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
                HttpResponseMessage httpResponseMessage = await _postService.DoPost(post);
                Console.WriteLine(httpResponseMessage.StatusCode);
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