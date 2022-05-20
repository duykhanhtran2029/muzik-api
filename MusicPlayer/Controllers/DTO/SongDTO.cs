using System;
using System.Linq;
using System.Collections.Generic;
using Database.MusicPlayer.Models;

namespace MusicPlayer.Controllers.DTO
{
    public partial class SongDTO
    {
        public SongDTO (Song s, ICollection<Artist> artists)
        {
            SongId = s.SongId;
            SongName = s.SongName;
            ThumbnailS = s.ThumbnailS;
            ThumbnailL = s.ThumbnailL;
            ThumbnailM = s.ThumbnailM;
            Likes = s.Likes;
            Link = s.Link;
            LinkBeat = s.LinkBeat;
            LinkLyric = s.LinkLyric;
            Downloads = s.Downloads;
            Duration = s.Duration;
            ReleaseDate = s.ReleaseDate;
            IsDeleted = s.IsDeleted;
            IsRecognizable = s.IsRecognizable;
            Artists = artists;
            Listens = s.Listens;
            ArtistsName = String.Join(", ", artists.Select(a => a.ArtistName).ToArray());
        }
        public string SongId { get; set; }
        public string SongName { get; set; }
        public string ThumbnailS { get; set; }
        public string ThumbnailM { get; set; }
        public string ThumbnailL { get; set; }
        public string Link { get; set; }
        public string LinkBeat { get; set; }
        public string LinkLyric { get; set; }
        public int? Duration { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? Likes { get; set; }
        public int? Downloads { get; set; }
        public int? Listens { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsRecognizable { get; set; }
        public string ArtistsName { get; set; }
        public ICollection<Artist> Artists { get; set; }
    }
}
