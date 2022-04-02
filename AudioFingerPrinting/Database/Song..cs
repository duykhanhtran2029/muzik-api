using MongoDB.Bson;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AudioFingerPrinting.Database
{
    public class Song
	{
		
		[BsonId]
		[DataMember]
		public MongoDB.Bson.ObjectId _id { get; set; }


		[DataMember]
		[BsonElement("ID")]
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

		[DataMember]
		[BsonElement("LinkLyric")]
		[JsonProperty("linkLyric")]
		public string LinkLyric { get; set; }

		[DataMember]
		[BsonElement("LinkBeat")]
		[JsonProperty("linkBeat")]
		public string LinkBeat { get; set; }


		[DataMember]
		[BsonElement("Like")]
		[JsonProperty("like")]
		public int Like { get; set; }

		[DataMember]
		[BsonElement("Duration")]
		[JsonProperty("like")]
		public int Duration { get; set; }

		[DataMember]
		[BsonElement("Genres")]
		[JsonProperty("genres")]
		private List<Genre> Genres;

		[DataMember]
		[BsonElement("Type")]
		[JsonProperty("type")]
		public int TypeID { get; set; }



		[DataMember]
		[BsonElement("ReleaseDate")]
		[JsonProperty("releaseDate")]
		public string ReleaseDate { get; set; }
	}
}
