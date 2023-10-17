using MicrobApp.Models;
using System.Text.Json;
using System;
using System.IO;
using System.Text.Json.Serialization;
//using Newtonsoft.Json;

namespace MicrobApp.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
        InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        string fileName;
        List<Post> posts = new List<Post>();


        string folderPath = FileSystem.AppDataDirectory;
        fileName = Path.Combine(folderPath, "post.json");
        string jsonData = File.ReadAllText(fileName);

        posts = JsonSerializer.Deserialize<List<Post>>(jsonData);
        
        // Configura la ListView para mostrar los posts
        postListView.ItemsSource = posts;
    }

    private void OnImageButtonClicked(object sender, EventArgs e)
    {
        // Navegar a la segunda página (SecondPage)
        Navigation.PushAsync(new PostPage());
    }

    private void Add_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new PostPage());
    }

    private void GoToUserPerfil(object sender, EventArgs e)
    {
        //Debe ir al perfil del usuario perteneciente a dichho post
        Navigation.PushAsync(new ProfilePage());
    }
}