using MongoDB.Bson;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Database
{
    public class Song
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
		[BsonElement("LinkLyric")]
		[JsonProperty("linkLyric")]
		public string LinkLyric { get; set; }

		[DataMember]
		[BsonElement("LinkBeat")]
		[JsonProperty("linkBeat")]
		public string LinkBeat { get; set; }


		[DataMember]
		[BsonElement("Thumbnail")]
		[JsonProperty("thumbnail")]
		public string Thumbnail { get; set; }

		[DataMember]
		[BsonElement("ThumbnailM")]
		[JsonProperty("thumbnailM")]
		public string ThumbnailM { get; set; }

		[DataMember]
		[BsonElement("ThumbnailL")]
		[JsonProperty("thumbnailL")]
		public string ThumbnailL { get; set; }


		[DataMember]
		[BsonElement("IsDeleted")]
		[JsonProperty("isDeleted")]
		public bool IsDeleted { get; set; }

		[DataMember]
		[BsonElement("Like")]
		[JsonProperty("like")]
		public uint like { get; set; }

		[DataMember]
		[BsonElement("Type")]
		[JsonProperty("type")]
		public uint Type { get; set; }

		[DataMember]
		[BsonElement("Duration")]
		[JsonProperty("duration")]
		public uint Duration { get; set; }

		[DataMember]
		[BsonElement("ReleaseDate")]
		[JsonProperty("releaseDate")]
		public string ReleaseDate { get; set; }


		[DataMember]
		[BsonElement("Genres")]
		[JsonProperty("genres")]
		public List<Genre> Genres;

	}
}
