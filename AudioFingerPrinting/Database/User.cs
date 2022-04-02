using MongoDB.Bson;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace AudioFingerPrinting.Database
{
	public class User
	{
		[BsonId]
		[DataMember]
		public MongoDB.Bson.ObjectId _id { get; set; }

		[DataMember]
		[BsonElement("userID")]
		public string UserID { get; set; }

		[DataMember]
		[BsonElement("userName")]
		public string UserName { get; set; }


		[DataMember]
		[BsonElement("password")]
		public string Password { get; set; }

		[DataMember]
		[BsonElement("name")]
		public string Name { get; set; }

		[DataMember]
		[BsonElement("phone")]
		public string Phone { get; set; }


		[DataMember]
		[BsonElement("email")]
		public string Email { get; set; }

		[DataMember]
		[BsonElement("avatar")]
		public string Avatar { get; set; }

		[DataMember]
		[BsonElement("role")]
		public string Role { get; set; }

		[DataMember]
		[BsonElement("isDeleted")]
		public bool IsDeleted { get; set; }


	}
}
