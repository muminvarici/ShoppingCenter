using ShoppingCenter.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.AppLayer.Models
{
	public class CartResponse : ICart
	{
		public List<Product> Items { get; set; }
	}
}
