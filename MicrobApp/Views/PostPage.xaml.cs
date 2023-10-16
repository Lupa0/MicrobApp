using System.IO.Enumeration;
using System.Text.Json;
using System.Text.Json.Serialization;

using MicrobApp.Models;
using System;
using System.IO;
//using Newtonsoft.Json;
using System.Globalization;
using System.Collections.Generic;

namespace MicrobApp.Views;



public partial class PostPage : ContentPage

{
    public PostPage()
	{
        InitializeComponent();
    }

    private void Publicar(object sender, EventArgs e)
    {
        string title = TextTitle.Text;
        string message = TextEditor.Text;

        if(title != "" && message != "")
        {
            string fileName;
            List<Post> posts = new List<Post>();
            
            string folderPath = FileSystem.AppDataDirectory;
            fileName = Path.Combine(folderPath, "post.json");
            string jsonData = File.ReadAllText(fileName);
           
            posts = JsonSerializer.Deserialize<List<Post>>(jsonData);
            
            var post = new Post()
            {
                Message = message,
                Title = title
            };

            posts.Add(post);

            // Serializar y guardar la lista actualizada
            jsonData = JsonSerializer.Serialize<List<Post>>(posts);
           
            // Serializar y guardar la lista actualizada
            File.WriteAllText(fileName, jsonData);  

        Navigation.PushAsync(new HomePage());

        }


    }
}