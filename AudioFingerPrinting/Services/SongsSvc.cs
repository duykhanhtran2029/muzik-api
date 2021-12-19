using AudioFingerPrinting.Database;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;


namespace AudioFingerPrinting.Servcies
{
    public class SongsSvc
    {
        private readonly IMongoCollection<Song> _songs;

        public SongsSvc(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _songs = database.GetCollection<Song>(settings.SongsCollectionName);
        }

        public List<Song> GetAll() =>
            _songs.Find(song => true).ToList();

        public Song GetById(uint songID)
        {
            Song song = _songs.Find<Song>(s => s.ID == songID).FirstOrDefault();
            return song;
        }

        public Song Create(Song song)
        {
            _songs.InsertOne(song);
            return song;
        }

    }
}
