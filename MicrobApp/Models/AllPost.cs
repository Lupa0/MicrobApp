using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrobApp.Models
{
    class AllPost
    {
        public ObservableCollection<Post> Posts { get; set; } = new ObservableCollection<Post>();

        public AllPost() => LoadPost();
        public void LoadPost()
        {
            Posts.Clear();

            string appDataPath = FileSystem.AppDataDirectory;

            IEnumerable<Post> posts = Directory
                                        .EnumerateFiles(appDataPath, "*.posts.txt")
                                        .Select(filename => new Post()
                                        {
                                            FileName = filename,
                                            Title = File.ReadAllText(filename),
                                            Message = File.ReadAllText(filename),
                                            Author = File.ReadAllText(filename)
                                        })
                                        .OrderBy(post => post.Author);
            foreach (var post in posts)
            {
                Posts.Add(post);
            }
        }
    }
}
