﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MusicPlayer.MusicPlayer.Models
{
    public partial class Genre
    {
        public Genre()
        {
            GenreSong = new HashSet<GenreSong>();
        }

        public string GenreId { get; set; }
        public string GenreName { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<GenreSong> GenreSong { get; set; }
    }
}
