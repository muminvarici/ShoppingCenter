using ShoppingCenter.InfraStructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.DataLayer.Models
{
	public class Cart : DocumentBase, ICart
	{
		public string UserId { get; set; }
		public List<Product> Items { get; set; }
	}

	public interface ICart
	{
		string UserId { get; set; }
		List<Product> Items { get; set; }
	}
}
