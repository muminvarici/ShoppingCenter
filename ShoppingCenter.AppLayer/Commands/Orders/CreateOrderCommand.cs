using MediatR;
using ShoppingCenter.AppLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.AppLayer.Commands.Orders
{
	public class CreateOrderCommand : OrderRequest, IRequest<OrderResponse>
	{
	}
}
