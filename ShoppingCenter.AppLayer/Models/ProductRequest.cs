using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ShoppingCenter.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.AppLayer.Models
{
	public class ProductRequest : IProductBase
	{
		public string Id { get; set; }
		public int Quantity { get; set; }
	}
	public class ProductSaveRequest : IProduct
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
	}
}
