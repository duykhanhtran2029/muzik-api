using MongoDB.Bson;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
namespace AudioFingerPrinting.Database
{
    public class Song
	{
		[BsonId]
		[BsonElement("_id")]
		public uint Id { get; set; }



		[DataMember]
		[BsonElement("Name")]
		public string Name { get; set; }


		[DataMember]
		[BsonElement("Title")]
		public string Title { get; set; }

		[DataMember]
		[BsonElement("Artist")]
		public string Artist { get; set; }

		[DataMember]
		[BsonElement("LinkZingMp3")]
		public string LinkZingMp3 { get; set; }


		[DataMember]
		[BsonElement("LinkMV")]
		public string LinkMV { get; set; }

		[DataMember]
		[BsonElement("Link")]
		public string Link { get; set; }


		[DataMember]
		[BsonElement("Thumbnail")]
		public string Thumbnail { get; set; }


		[DataMember]
		[BsonElement("IsDeleted")]
		public bool IsDeleted { get; set; }

	}
}
