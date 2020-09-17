using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCenter.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public abstract class ApiControllerBase : ControllerBase
	{
		[NonAction]
		public virtual IActionResult ConvertToResponse(object result)
		{
			return result != null ? (IActionResult)Ok(result) : NotFound();
		}
	}
}
