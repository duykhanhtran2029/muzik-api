using AudioFingerPrinting.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioFingerPrinting.DTO
{
    public class MatchedSong
    {
        public MatchedSong(Song song, double score)
        {
            this.song = song;
            this.score = score;
        }
        public Song song;
        public double score;
    }
    public class FingerPrinting_ResultDTO
    {
        public FingerPrinting_ResultDTO(long time)
        {
            this.time = time;
            this.matchedSongs = new List<MatchedSong>();
        }
        public List<MatchedSong> matchedSongs;
        public long time;
    }
}
