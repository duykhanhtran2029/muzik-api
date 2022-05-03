using Database;
using CoreLib.AudioFormats;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using CoreLib;

namespace MusicPlayer.Servcies
{
    public class SongsSvc
    {
        public readonly IMongoCollection<Fingerprint> _fingerprints;
        public readonly IMongoCollection<Song> _songs;

        public SongsSvc(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _fingerprints = database.GetCollection<Fingerprint>(settings.TFPsCollectionName);
            _songs = database.GetCollection<Song>(settings.SongsCollectionName);
        }

        public async Task<List<Song>> GetAsync() =>
            await _songs.Find(song => true).ToListAsync();

        public async Task<Song> GetAsync(uint id) =>
            await _songs.Find(song => song.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Song newSong) =>
                await _songs.InsertOneAsync(newSong);
        public async Task UpdateAsync(uint id, Song updatedSong) =>
            await _songs.ReplaceOneAsync(x => x.Id == id, updatedSong);

        public async Task RemoveAsync(uint id) => 
            await _songs.DeleteOneAsync(x => x.Id == id);

        /// <summary>
        /// <para>Add a new song to the database.</para>
        /// <para>WARNING: Song must be sampled at 48000Hz!</para>
        /// </summary>
        /// <param name="input">wave audio file as bytes array</param>
        public void AddNewSong(byte[] input, uint songID)
        {
            List<TimeFrequencyPoint> TimeFrequencyPoints = RecognizerHelper.Processing(input);
            Fingerprint ftp = new Fingerprint(songID, TimeFrequencyPoints);
            _fingerprints.InsertOne(ftp);
        }

        public uint newId()
        {
            var id = _songs.AsQueryable().OrderByDescending(c => c.Id).First().Id + 1;
            return (uint)Convert.ToInt32(id);
        }

    }
}
