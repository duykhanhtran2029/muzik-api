using MongoDB.Bson;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AudioFingerPrinting.Database
{
	public class ArtistSong
	{

        [BsonId]
        [DataMember]
        public MongoDB.Bson.ObjectId _id { get; set; }


        [DataMember]
        [BsonElement("artistID")]
        public string ArtistID;


        [DataMember]
        [BsonElement("songs")]
        public List<Song> Songs;
    }
}
