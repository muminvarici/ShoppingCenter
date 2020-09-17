using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DependencyInjection.Attributes
{
	public class BsonCollectionAttribute : Attribute
	{
		public string CollectionName { get; set; }
	}
}
