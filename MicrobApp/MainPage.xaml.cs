namespace MicrobApp;
using MicrobApp.Views;
public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private void Login_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new HomePage());
    }
}