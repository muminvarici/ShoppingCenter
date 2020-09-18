using Core.DependencyInjection.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ShoppingCenter.Api.Filters
{
	public class ValidationFilter : ExceptionFilterAttribute, IExceptionFilter
	{
		public override void OnException(ExceptionContext context)
		{
			if (context.Exception is ValidationException || context.Exception is ServiceException)
			{
				context.HttpContext.Response.ContentType = "application/json";
				context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

				if (context.Exception is ValidationException)
				{
					context.Result = new JsonResult(
					((ValidationException)context.Exception).Errors);
				}
				if (context.Exception is ServiceException)
				{
					context.Result = new JsonResult(context.Exception.Message);
				}

				return;
			}
		}
	}
}
