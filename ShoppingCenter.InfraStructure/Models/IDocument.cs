using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ShoppingCenter.InfraStructure.Models
{
	public interface IDocument
	{
		[BsonId]
		ObjectId Id { get; set; }
		[BsonRepresentation(BsonType.DateTime)]
		DateTime CreatedAt { get; set; }
		[BsonRepresentation(BsonType.DateTime)]
		DateTime? UpdatedAt { get; set; }
	}
}
