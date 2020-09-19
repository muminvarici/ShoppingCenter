using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.InfraStructure
{
	public class ObjectIdConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			serializer.Serialize(writer, value.ToString());
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			ObjectId id = ObjectId.Empty;
			ObjectId.TryParse(reader?.Value?.ToString(), out id);
			return id;
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(ObjectId).IsAssignableFrom(objectType);
			//return true;
		}
	}
}
