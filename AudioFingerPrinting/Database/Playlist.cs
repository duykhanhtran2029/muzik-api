using MongoDB.Bson;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace AudioFingerPrinting.Database
{
	public class Playlist
	{
		[BsonId]
		[DataMember]
		public MongoDB.Bson.ObjectId _id { get; set; }

		[DataMember]
		[BsonElement("id")]
		public string ID { get; set; }


		[DataMember]
		[BsonElement("listen")]
		public uint Listen { get; set; }


		[DataMember]
		[BsonElement("like")]
		public uint Like { get; set; }

		[DataMember]
		[BsonElement("title")]
		public string Title { get; set; }

		[DataMember]
		[BsonElement("isDeleted")]
		public bool IsDeleted { get; set; }



		[DataMember]
		[BsonElement("thumbnail")]
		public string Thumbnail { get; set; }


		[DataMember]
		[BsonElement("sortDescription")]
		public string SortDescription { get; set; }



		[DataMember]
		[BsonElement("releaseDate")]
		public string ReleaseDate { get; set; }


		[DataMember]
		[BsonElement("userID")]
		public string UserID { get; set; }

		[DataMember]
		[BsonElement("isPrivate")]
		public bool IsPrivate { get; set; }


	}
}
