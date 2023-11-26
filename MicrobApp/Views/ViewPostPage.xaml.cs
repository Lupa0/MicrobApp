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
<<<<<<< Updated upstream
=======

    protected override void OnAppearing()
    {
        base.OnAppearing();
      //  LoadComments();
    }
  /*  private async void LoadComments()
    {
        commenListView.ItemsSource = null;
        comments.Clear();
        try
        {
            comments = this._post.Comments;
            commenListView.ItemsSource = comments.Reverse();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al obtener los comments: " + ex.Message);
            await DisplayAlert("Error", "Ha ocurrido un problema. Por favor vuelve a intentar mas tarde.", "OK");
        }
    }*/

    

>>>>>>> Stashed changes
    private void GoToUserPerfil(object sender, EventArgs e)
    {

    }

    private void Button_Clicked(object sender, EventArgs e)
    {

    }
}