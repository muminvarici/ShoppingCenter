using ShoppingCenter.InfraStructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.DataLayer.Models
{
	public class Cart : DocumentBase, ICart
	{
		public List<Product> Items { get; set; }
	}

	public interface ICart
	{
		List<Product> Items { get; set; }
	}
}
