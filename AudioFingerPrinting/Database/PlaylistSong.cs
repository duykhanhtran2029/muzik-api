using MongoDB.Bson;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AudioFingerPrinting.Database
{
	public class PlaylistSong
	{

        [BsonId]
        [DataMember]
        public MongoDB.Bson.ObjectId _id { get; set; }


        [DataMember]
        [BsonElement("playlistID")]
        public string PlaylistID;


        [DataMember]
        [BsonElement("songs")]
        public List<Song> Songs;


    }
}
