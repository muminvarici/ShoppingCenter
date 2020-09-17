using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingCenter.AppLayer.Queries;
using ShoppingCenter.DataLayer.Models;
using System;
using System.Threading.Tasks;

namespace ShoppingCenter.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CartController : ControllerBase
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

		private IActionResult ConvertToResponse(object result)
		{
			return result != null ? (IActionResult)Ok(result) : NotFound();
		}
	}
}
