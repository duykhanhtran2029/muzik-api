using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Database.MusicPlayer.Models
{
    public partial class Playlist
    {
        public Playlist()
        {
            PlaylistSong = new HashSet<PlaylistSong>();
        }

        public string PlaylistId { get; set; }
        public string PlaylistName { get; set; }
        public string UserId { get; set; }
        public bool IsDeleted { get; set; }
        [JsonIgnore]
        public virtual ICollection<PlaylistSong> PlaylistSong { get; set; }
    }
}
