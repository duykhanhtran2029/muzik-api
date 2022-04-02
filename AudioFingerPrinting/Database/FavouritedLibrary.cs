using MongoDB.Bson;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AudioFingerPrinting.Database
{
	public class FavouritedLibrary
	{
		[BsonId]
		[DataMember]
		public MongoDB.Bson.ObjectId _id { get; set; }

		[DataMember]
		[BsonElement("userID")]
		public string UserID { get; set; }


		[DataMember]
		[BsonElement("artistFollowed")]
		public List<string> artistFollowed { get; set; }


		[DataMember]
		[BsonElement("playlistLibrary")]
		public List<string> playlistLibrary { get; set; }



		[DataMember]
		[BsonElement("songFavourited")]
		public List<string> songFavourited { get; set; }


	}
}
