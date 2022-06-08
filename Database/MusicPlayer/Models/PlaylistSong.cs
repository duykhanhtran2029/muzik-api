using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Database.MusicPlayer.Models
{
    public partial class PlaylistSong
    {
        public string PlaylistId { get; set; }
        public string SongId { get; set; }

        [JsonIgnore]
        public virtual Playlist Playlist { get; set; }
        [JsonIgnore]
        public virtual Song Song { get; set; }
    }
}
