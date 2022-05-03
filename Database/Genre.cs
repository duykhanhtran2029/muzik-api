using MongoDB.Bson;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Database
{
    public class Genre
    {
        [BsonId]
        [DataMember]
        public ObjectId _id { get; set; }


        [DataMember]
        [BsonElement("id")]
        public string Id;


        [DataMember]
        [BsonElement("name")]
        [JsonProperty("name")]
        public string Name { get; set; }


        [DataMember]
        [BsonElement("isDeleted")]
        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }
    }
}
