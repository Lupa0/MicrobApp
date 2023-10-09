using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrobApp.Models
{
    public class UserProfile
    {
        public string ProfileImage { get; set; } 
        public string CoverImage { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Biography { get; set; }
        public DateOnly JoinDate { get; set; }
        public int Followers { get; set; }
        public int Following { get; set; }
        public bool IsFollowing { get; set; }
    }
}
