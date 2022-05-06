using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Database.Models
{
    public partial class Album
    {
        public string AlbumId { get; set; }
        public string AlbumName { get; set; }
        public string ThumbnailS { get; set; }
        public string ThumbnailM { get; set; }
        public string ThumbnailL { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
