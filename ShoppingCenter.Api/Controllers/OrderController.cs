using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingCenter.AppLayer.Commands.Orders;
using ShoppingCenter.AppLayer.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCenter.Api.Controllers
{
	public class OrderController : ApiControllerBase
	{
		private readonly IMediator mediator;

		public OrderController(IMediator mediator)
		{
			this.mediator = mediator;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(string id)
		{
			var query = new GetOrderByIdQuery { OrderId = id };
			var result = await mediator.Send(query);
			return ConvertToResponse(result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateOrder([FromBody]CreateOrderCommand command)
		{
			var result = await mediator.Send(command);
			return ConvertToResponse(result);
		}
	}
}
