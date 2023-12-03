namespace MicrobApp.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Text { get; set; }
        public string Attachment { get; set; }
        public UserProfile UserOwner { get; set; } = null!;
        public bool IsSanctioned { get; set; }
        public DateTime Created { get; set; }
        public bool Active { get; set; }
        //Respuestas a un post
        public ICollection<Post> Comments { get; set; } = new List<Post>();
        //Personas que le dieron me gusta al post
        public List<UserProfile> Likes { get; set; } = new List<UserProfile>();
        public bool IsLiked
        {
            get => Likes.Exists(aUser => Equals(aUser.UserName, SecureStorage.GetAsync("username").Result));
        }
        public List<string> Hashtag { get; set; } = new List<string>();
    }
}
