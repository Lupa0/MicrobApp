using MicrobApp.Models;
using MicrobApp.Services;

namespace MicrobApp.Views;

public partial class PostPage : ContentPage
{
    private Post post = new Post();
    //private MemoryStream selectedImageStream;


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
            if (message != "")
                this.post.Text = message;

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

    private void Button_Clicked(object sender, EventArgs e)
    {

    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {

    }

    private async void Button_camera(object sender, EventArgs e)
    {
        var photo = await MediaPicker.CapturePhotoAsync();
        if (photo != null)
        {
            var selectedImageStream = (MemoryStream)await photo.OpenReadAsync();
            imgPhoto.Source = ImageSource.FromStream(() => selectedImageStream);
        }
    }

    private async void Button_library(object sender, EventArgs e)
    {
        var photo = await MediaPicker.PickPhotoAsync();
        if (photo != null)
        {
            using (MemoryStream selectedImageStream = new MemoryStream())
            {
                // Abre el archivo seleccionado y copia los datos al MemoryStream
                using (Stream stream = await photo.OpenReadAsync())
                {
                    await stream.CopyToAsync(selectedImageStream);
                }

                // Convierte el MemoryStream a una cadena Base64
                byte[] imageBytes = selectedImageStream.ToArray();
                string base64Image = Convert.ToBase64String(imageBytes);
                base64Image = "data:image/jpg;base64," + base64Image;
                    ;
                // Asigna la cadena Base64 a la propiedad Attachment del objeto Post
                this.post.Attachment = base64Image;

                // Muestra la imagen en el componente imgPhoto
                imgPhoto.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes));
            }
        }
    }
}