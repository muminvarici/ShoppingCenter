using MediatR;
using MongoDB.Bson;
using ShoppingCenter.AppLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.AppLayer.Commands.Carts
{
	public class AddItemToCartCommand : IRequest<CartResponse>
	{
		public string UserId { get; set; }
		public string Id { get; set; }
		public ProductRequest ProductInfo { get; set; }
	}
}
