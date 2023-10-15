using System.IO.Enumeration;
using System.Text.Json;
using MicrobApp.Models;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Globalization;

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
            // Leer el archivo JSON existente (si existe)
            string fileName = "post.json";
            List<Post> posts = new List<Post>();
            string jsonData = "";

            if (File.Exists(fileName))
            {
                jsonData = File.ReadAllText(fileName);
                posts = JsonConvert.DeserializeObject<List<Post>>(jsonData);
            }

            var post = new Post()
            {
                //Date = ,
                Message = message,
                Title = title
            };

            posts.Add(post);

            // Serializar y guardar la lista actualizada
           string folderPath = FileSystem.AppDataDirectory;
            fileName = Path.Combine(folderPath, "post.json");
            jsonData = JsonConvert.SerializeObject(posts);
            // Serializar y guardar la lista actualizada
            //File.AppendText(jsonData);
            File.WriteAllText(fileName, jsonData);  


        // Combinar la ruta con el nombre del archivo

        // Guardar los datos en un archivo JSON en el sistema de almacenamiento local
        //File.WriteAllText(fileName, jsonData);
        Navigation.PushAsync(new HomePage());

        }


    }
}