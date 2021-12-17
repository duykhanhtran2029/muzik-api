using MongoDB.Bson;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.Attributes;
namespace AudioFingerPrinting.Database
{
    public class Song
	{
		/// <summary>
		/// Instance of a song
		/// </summary>
		/// <param name="id">song ID</param>
		/// <param name="name">Name of the song</param>
		public Song(uint id, string name)
		{
			ID = id;
			Name = name;
		}

		[BsonId]
		[BsonElement("_id")]
		public uint ID { get; set; }



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

	}
}
