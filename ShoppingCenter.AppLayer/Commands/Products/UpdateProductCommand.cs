using MediatR;
using ShoppingCenter.AppLayer.Models;
using ShoppingCenter.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.AppLayer.Commands.Products
{
	public class UpdateProductCommand : ProductSaveRequest, IRequest<ProductResponse>
	{
		public string Id { get; set; }
	}
}
