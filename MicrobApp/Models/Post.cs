using System.Globalization;

namespace MicrobApp.Models
{
    public class Post
    {
        public string FileName { get; set; }
        public string Id { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        //public UserPerfil perfil { get; set; }

        public string Author { get; set; } = "Ana Chi";
        public string AuthorId { get; set; } = "ana_chi@perrito";
        //public string AuthorName { get; set; }
        //= string.Empty;
        //public string AuthorUrl { get; set; }
        public string PhotoUrl { get; set; } = "AnaChi";

        public Calendar Date { get; set; }

        public Post() { }
    }
}
