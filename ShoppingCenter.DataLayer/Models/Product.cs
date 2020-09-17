using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ShoppingCenter.InfraStructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.DataLayer.Models
{
	public class Product : DocumentBase, IProduct
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
	}

	public interface IProduct : IProductBase
	{
		string Name { get; set; }
		decimal Price { get; set; }
	}

	public interface IProductBase
	{
		int Quantity { get; set; }
	}
}
