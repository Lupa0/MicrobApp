using System.Globalization;

namespace MicrobApp.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Text { get; set; }
        public string Attachment { get; set; }
        public UserProfile UserOwner { get; set; } = null!;
        public bool isSanctioned { get; set; } = false;
        public DateTime Created { get; set; }

        //Personas que le dieron me gusta al post
        public ICollection<UserProfile> Likes { get; set; } = new List<UserProfile>();
        public List<string> Hashtag { get; set; } = new List<string>();
    }
}
