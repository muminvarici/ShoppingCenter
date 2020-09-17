using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ShoppingCenter.InfraStructure
{
	public interface IDocument
	{
		[BsonId]
		ObjectId _id { get; set; }
		[BsonRepresentation(BsonType.DateTime)]
		DateTime CreatedAt { get; set; }
		[BsonRepresentation(BsonType.DateTime)]
		DateTime? UpdatedAt { get; set; }
	}
}
