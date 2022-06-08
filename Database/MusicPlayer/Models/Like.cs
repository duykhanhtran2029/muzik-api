using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Database.MusicPlayer.Models
{
    public partial class Like
    {
        public string UserId { get; set; }
        public string SongId { get; set; }
        [JsonIgnore]
        public virtual Song Song { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}
