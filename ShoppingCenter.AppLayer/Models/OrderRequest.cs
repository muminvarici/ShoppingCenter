using ShoppingCenter.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.AppLayer.Models
{
	public class OrderRequest
	{
		public string UserId { get; set; }
		public PaymentDetail PaymentDetail { get; set; }
		public AddressDetail Address { get; set; }
	}
}
