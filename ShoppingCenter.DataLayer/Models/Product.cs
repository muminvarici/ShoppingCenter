using ShoppingCenter.InfraStructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.DataLayer.Models
{
	public class Product : DocumentBase,IProduct
	{
		public string Name { get; set; }
	}

	public interface IProduct
	{
	}
}
