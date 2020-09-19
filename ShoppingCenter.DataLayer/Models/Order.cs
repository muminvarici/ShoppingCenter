using ShoppingCenter.InfraStructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.DataLayer.Models
{
	public class Order : DocumentBase
	{
		public string UserId { get; set; }
		public string CartId { get; set; }
		public PaymentDetail PaymentDetail { get; set; }
		public AddressDetail Address { get; set; }
	}

	//todo store in db
	public class AddressDetail
	{
		public int CityCode { get; set; }
		public int DistrictCode { get; set; }
		public string AddressDetails { get; set; }
	}

	public enum PaymentMethod
	{
		CreditCart = 1,
		Cash = 2,
		BankTransfer = 3
	}

	public enum CurrencyCode
	{
		TRY = 1,
		EUR = 2,
		USD = 3
	}

	public class PaymentDetail
	{
		public PaymentMethod PaymentMethod { get; set; }
		public decimal Amount { get; set; }
		public CurrencyCode CurrencyCode { get; set; }
	}
}
