using MongoDB.Bson;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace AudioFingerPrinting.Database
{
    public class Song
	{
		[BsonId]
		[BsonElement("_id")]
		[JsonProperty("id")]
		public uint Id { get; set; }

		[DataMember]
		[BsonElement("Name")]
		[JsonProperty("name")]
		public string Name { get; set; }

		[DataMember]
		[BsonElement("Title")]
		[JsonProperty("title")]
		public string Title { get; set; }

		[DataMember]
		[BsonElement("Artist")]
		[JsonProperty("artist")]
		public string Artist { get; set; }

		[DataMember]
		[BsonElement("LinkZingMp3")]
		[JsonProperty("linkZingMp3")]
		public string LinkZingMp3 { get; set; }

		[DataMember]
		[BsonElement("LinkMV")]
		[JsonProperty("linkMV")]
		public string LinkMV { get; set; }

		[DataMember]
		[BsonElement("Link")]
		[JsonProperty("link")]
		public string Link { get; set; }


		[DataMember]
		[BsonElement("Thumbnail")]
		[JsonProperty("thumbnail")]
		public string Thumbnail { get; set; }


		[DataMember]
		[BsonElement("IsDeleted")]
		[JsonProperty("isDeleted")]
		public bool IsDeleted { get; set; }

	}
}
