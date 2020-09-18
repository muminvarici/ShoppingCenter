using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingCenter.AppLayer.Commands.Products;
using ShoppingCenter.AppLayer.Queries;
using System.Threading.Tasks;

namespace ShoppingCenter.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ApiControllerBase
	{
		private readonly IMediator mediator;

		public ProductController(IMediator mediator)
		{
			this.mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var result = await mediator.Send(new GetAllProductsQuery());
			return ConvertToResponse(result);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody]AddProductCommand command)
		{
			var result = await mediator.Send(command);
			return ConvertToResponse(result);
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody]UpdateProductCommand command)
		{
			var result = await mediator.Send(command);
			return ConvertToResponse(result);
		}
	}
}