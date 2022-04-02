using MongoDB.Bson;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace AudioFingerPrinting.Database
{
	public class Type
	{
		[BsonId]
		[DataMember]
		public MongoDB.Bson.ObjectId _id { get; set; }


		[DataMember]
		[BsonElement("id")]
		public int ID { get; set; }

		[DataMember]
		[BsonElement("title")]
		public string Title { get; set; }

		[DataMember]
		[BsonElement("isDeleted")]
		public bool IsDeleted { get; set; }


	}
}
