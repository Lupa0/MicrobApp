
namespace MicrobApp.Models
{
    public class UserProfile
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProfileImage { get; set; }
        public string Biography { get; set; }
        public string Occupation { get; set; }
        public string City { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<UserProfile> FollowUsers { get; set; } = new List<UserProfile>();
        public ICollection<Post> Posts { get; set; } = new List<Post>();

        public int Followers { get; set; } = 0;
        public int Following { get; set; } = 0;
        public bool IsFollowing { get; set; } = false;
    }
}
