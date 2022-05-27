using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MusicPlayer.MusicPlayer.Models
{
    public partial class Artist
    {
        public Artist()
        {
            ArtistSong = new HashSet<ArtistSong>();
        }

        public string ArtistId { get; set; }
        public string ArtistName { get; set; }
        public string ThumbnailS { get; set; }
        public string ThumbnailM { get; set; }
        public string ThumbnailL { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<ArtistSong> ArtistSong { get; set; }
    }
}
