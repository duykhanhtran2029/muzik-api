using MongoDB.Bson;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Database.AudioFingerPrinting
{
    public class RecognizableSong
	{
		[BsonId]
		[BsonElement("_id")]
		public ObjectId _Id { get; set; }

		[BsonElement("ID")]
		[JsonProperty("id")]
		public uint Id { get; set; }

		[DataMember]
		[BsonElement("Name")]
		[JsonProperty("name")]
		public string Name { get; set; }

	}
}
