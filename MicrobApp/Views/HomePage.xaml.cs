using MicrobApp.Models;
using System.Text.Json;
using System;
using System.IO;
using Newtonsoft.Json;

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
        string fileName = "posts.json";
        List<Post> posts = new List<Post>();

        if (File.Exists(fileName))
        {
            string jsonData = File.ReadAllText(fileName);
            posts = JsonConvert.DeserializeObject<List<Post>>(jsonData);
        }
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


}