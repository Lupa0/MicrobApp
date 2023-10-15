using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace MicrobApp.Models
{
    internal class Post
    {
        public string FileName { get; set; }
        public string Id { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        //public Perfil perfil { get; set; }

            public string Author { get; set; }
            public string AuthorId { get; set; }
        = Guid.NewGuid().ToString();
        public string AuthorName { get; set; }
        = string.Empty;
        public string AuthorUrl { get; set; }
        public string PhotoUrl { get; set; }

        public Calendar Date { get; set; }

        public Post() { }
    }
}
