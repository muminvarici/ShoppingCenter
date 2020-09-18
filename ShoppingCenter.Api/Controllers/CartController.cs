using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingCenter.AppLayer.Commands;
using ShoppingCenter.AppLayer.Queries;
using ShoppingCenter.DataLayer.Models;
using System;
using System.Threading.Tasks;

namespace ShoppingCenter.Api.Controllers
{

	public class CartController: ApiControllerBase
	{
		private readonly IMediator mediator;

		public CartController(IMediator mediator)
		{
			this.mediator = mediator;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetAsync(string id)
		{
			var query = new GetCartByIdQuery { Id = id };
			var result = await mediator.Send(query);
			return ConvertToResponse(result);
		}

		[HttpGet("GetBuUserId/{userId}")]
		public async Task<IActionResult> GetBuUserIdAsync(string userId)
		{
			var query = new GetCartByUserIdQuery { UserId = userId };
			var result = await mediator.Send(query);
			return ConvertToResponse(result);
		}


		[HttpPost]
		public async Task<IActionResult> AddItemToCartAsync([FromBody] AddItemToCartCommand command)
		{
			var result = await mediator.Send(command);
			return ConvertToResponse(result);
		}

	}
}
