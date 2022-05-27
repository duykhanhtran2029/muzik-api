using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MusicPlayer.MusicPlayer.Models
{
    public partial class ArtistSong
    {
        public string ArtistId { get; set; }
        public string SongId { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual Song Song { get; set; }
    }
}
