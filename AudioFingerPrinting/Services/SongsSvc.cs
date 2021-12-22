using AudioFingerPrinting.Database;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<List<Song>> GetAsync() =>
            await _songs.Find(song => !song.IsDeleted).ToListAsync();

        public async Task<Song> GetAsync(uint id) =>
            await _songs.Find(song => song.Id == id && !song.IsDeleted).FirstOrDefaultAsync();

        public async Task CreateAsync(Song newBook) =>
                await _songs.InsertOneAsync(newBook);
        public async Task UpdateAsync(uint id, Song updatedSong) =>
            await _songs.ReplaceOneAsync(x => x.Id == id, updatedSong);

        public async Task RemoveAsync(uint id) =>
            await _songs.DeleteOneAsync(x => x.Id == id);

    }
}
