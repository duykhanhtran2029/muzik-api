using MongoDB.Bson;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace AudioFingerPrinting.Database
{
	public class Genre
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
		[BsonElement("isDeleted")]
		public bool IsDeleted { get; set; }


	}
}