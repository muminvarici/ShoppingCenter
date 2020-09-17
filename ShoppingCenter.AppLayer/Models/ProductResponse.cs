using ShoppingCenter.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.AppLayer.Models
{
	public class ProductResponse : IProduct
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
	}
}
