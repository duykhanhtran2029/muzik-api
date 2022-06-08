using System;
using System.Linq;
using System.Collections.Generic;
using Database.MusicPlayer.Models;

namespace MusicPlayer.Controllers.DTO
{
    public partial class ArtistDTO
    {
        public ArtistDTO (Artist a, ICollection<Song> songs)
        {
            ArtistId = a.ArtistId;
            ArtistName = a.ArtistName;
            Thumbnail = a.Thumbnail;
            Likes = songs.Sum(x => x.Likes);
            Downloads = songs.Sum(x => x.Downloads);
            Listens = songs.Sum(x => x.Listens);
            NumberOfSongs = songs.Count;
            IsDeleted = a.IsDeleted;
        }
        public string ArtistId { get; set; }
        public string ArtistName { get; set; }
        public string Thumbnail { get; set; }
        public int? Likes { get; set; }
        public int? Downloads { get; set; }
        public int? Listens { get; set; }
        public int? NumberOfSongs { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
