namespace MicrobApp.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private void OnImageButtonClicked(object sender, EventArgs e)
    {
        // Navegar a la segunda página (SecondPage)
        Navigation.PushAsync(new PostPage());
    }
}