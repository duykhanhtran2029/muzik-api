using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Database.MusicPlayer.Models
{
    public partial class Song
    {
        public Song()
        {
            ArtistSong = new HashSet<ArtistSong>();
            GenreSong = new HashSet<GenreSong>();
            Like = new HashSet<Like>();
            PlaylistSong = new HashSet<PlaylistSong>();
        }

        public string SongId { get; set; }
        public string SongName { get; set; }
        public string Thumbnail { get; set; }
        public string Link { get; set; }
        public string LinkBeat { get; set; }
        public string LinkLyric { get; set; }
        public int? Duration { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? Likes { get; set; }
        public int? Downloads { get; set; }
        public int? Listens { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsRecognizable { get; set; }

        public virtual ICollection<ArtistSong> ArtistSong { get; set; }
        public virtual ICollection<GenreSong> GenreSong { get; set; }
        public virtual ICollection<Like> Like { get; set; }
        public virtual ICollection<PlaylistSong> PlaylistSong { get; set; }
    }
}
