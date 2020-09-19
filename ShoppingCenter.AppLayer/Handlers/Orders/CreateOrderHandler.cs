using AutoMapper;
using MediatR;
using ShoppingCenter.AppLayer.Commands.Orders;
using ShoppingCenter.AppLayer.Models;
using ShoppingCenter.DataLayer.Models;
using ShoppingCenter.DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingCenter.AppLayer.Handlers.Orders
{
	public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OrderResponse>
	{
		private readonly IMapper mapper;
		private readonly OrderService orderService;
		private readonly ICartService cartService;

		public CreateOrderHandler(IMapper mapper, OrderService orderService, ICartService cartService)
		{
			this.mapper = mapper;
			this.orderService = orderService;
			this.cartService = cartService;
		}
		public async Task<OrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			var order = mapper.Map<Order>(request);
			await orderService.CreateOrderAsync(order);

			var result = mapper.Map<OrderResponse>(order);
			result.Cart = mapper.Map<CartResponse>(await cartService.GetByIdAsync(order.CartId));

			return result;
		}
	}
}
