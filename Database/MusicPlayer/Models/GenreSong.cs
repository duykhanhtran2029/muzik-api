using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Database.MusicPlayer.Models
{
    public partial class GenreSong
    {
        public string GenreId { get; set; }
        public string SongId { get; set; }

        [JsonIgnore]
        public virtual Genre Genre { get; set; }
        [JsonIgnore]
        public virtual Song Song { get; set; }
    }
}
