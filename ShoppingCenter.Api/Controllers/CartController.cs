using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingCenter.AppLayer.Commands.Carts;
using ShoppingCenter.AppLayer.Queries;
using System.Threading.Tasks;

namespace ShoppingCenter.Api.Controllers
{

	public class CartController : ApiControllerBase
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

		[HttpGet("GetByUserId/{userId}")]
		public async Task<IActionResult> GetByUserIdAsync(string userId)
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

		[HttpDelete("{id}")]
		public async Task DeleteAsync(string id)
		{
			var command = new DeleteCartCommand { Id = id };
			await mediator.Send(command);
		}
	}
}
