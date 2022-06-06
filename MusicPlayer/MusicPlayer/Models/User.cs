using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MusicPlayer.MusicPlayer.Models
{
    public partial class User
    {
        public User()
        {
            History = new HashSet<History>();
            Like = new HashSet<Like>();
        }

        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<History> History { get; set; }
        public virtual ICollection<Like> Like { get; set; }
    }
}
