using AudioFingerPrinting.Database;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;


namespace AudioFingerPrinting.Servcies
{
    public class FingerprintsSvc
    {
        private readonly IMongoCollection<Fingerprint> _fingerprints;

        public FingerprintsSvc(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _fingerprints = database.GetCollection<Fingerprint>(settings.TFPsCollectionName);
        }

        public List<Fingerprint> Get() =>
            _fingerprints.Find(Fingerprint => true).ToList();

        public Fingerprint Get(uint songID)
        {
            //uint uSongID = Convert.ToUInt32(songID);
            Fingerprint fingerprint = _fingerprints.Find<Fingerprint>(fingerprint => fingerprint.songID == songID).FirstOrDefault();
            return fingerprint;
        }


        public Fingerprint Create(Fingerprint fingerprint)
        {
            _fingerprints.InsertOne(fingerprint);
            return fingerprint;
        }
    }
}
