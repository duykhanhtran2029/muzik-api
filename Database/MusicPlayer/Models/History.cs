using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Database.MusicPlayer.Models
{
    public partial class History
    {
        public string UserId { get; set; }
        public string SongId { get; set; }
        public int Count { get; set; }

        public virtual Song Song { get; set; }
    }
}
