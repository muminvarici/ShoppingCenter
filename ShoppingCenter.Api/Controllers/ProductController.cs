using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCenter.AppLayer.Models;
using ShoppingCenter.AppLayer.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
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

	}
}