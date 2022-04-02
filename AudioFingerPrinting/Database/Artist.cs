using MongoDB.Bson;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace AudioFingerPrinting.Database
{
	public class Artist
	{
		[BsonId]
		[DataMember]
		public MongoDB.Bson.ObjectId _id { get; set; }


		[DataMember]
		[BsonElement("id")]
		public string ID { get; set; }


		[DataMember]
		[BsonElement("name")]
		public string Name { get; set; }


		[DataMember]
		[BsonElement("alias")]
		public string Alias { get; set; }



		[DataMember]
		[BsonElement("thumbail")]
		public string Thumbnail { get; set; }



		[DataMember]
		[BsonElement("thumbnailM")]
		public string ThumbNailM { get; set; }



		[DataMember]
		[BsonElement("isDeleted")]
		public bool IsDeleted { get; set; }

	}
}