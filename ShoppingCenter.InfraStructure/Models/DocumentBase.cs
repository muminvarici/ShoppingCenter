using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShoppingCenter.InfraStructure.Models
{
	public abstract class DocumentBase : IDocument
	{
		[BsonId]
		public ObjectId Id { get; set; }
		[BsonRepresentation(BsonType.DateTime)]
		public DateTime CreatedAt { get; set; }
		[BsonRepresentation(BsonType.DateTime)]
		public DateTime? UpdatedAt { get; set; }
	}
}
