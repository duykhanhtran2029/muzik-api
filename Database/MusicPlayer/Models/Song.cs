using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Database.Models
{
    public partial class Song
    {
        public string SongId { get; set; }
        public string SongName { get; set; }
        public string ThumbnailS { get; set; }
        public string ThumbnailM { get; set; }
        public string ThumbnailL { get; set; }
        public string Link { get; set; }
        public string LinkBeat { get; set; }
        public string LinkLyric { get; set; }
        public int? Duration { get; set; }
        public int? Type { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? Likes { get; set; }
        public int? Downloads { get; set; }
        public int? Listens { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
