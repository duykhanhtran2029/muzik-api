using Database.AudioFingerPrinting;
using System.Collections.Generic;

namespace CoreLib
{
    public class MatchedSong
    {
        public MatchedSong(RecognizableSong song, double score)
        {
            this.song = song;
            this.score = score;
        }
        public RecognizableSong song;
        public double score;
    }
    public class FingerPrintingResult
    {
        public FingerPrintingResult(long time)
        {
            this.time = time;
            this.matchedSongs = new List<MatchedSong>();
        }
        public List<MatchedSong> matchedSongs;
        public long time;
    }
}
