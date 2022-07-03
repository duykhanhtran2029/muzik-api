using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Database.MusicPlayer.Models
{
    public partial class Artist
    {
        public Artist()
        {
            ArtistSong = new HashSet<ArtistSong>();
        }

        public string ArtistId { get; set; }
        public string ArtistName { get; set; }
        public string Thumbnail { get; set; }
        public bool IsDeleted { get; set; }
        [JsonIgnore]
        public virtual ICollection<ArtistSong> ArtistSong { get; set; }
    }
}
