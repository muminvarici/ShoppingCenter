using FluentValidation;
using MongoDB.Bson;
using ShoppingCenter.AppLayer.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.AppLayer.Validators
{
	public class GetOrderByIdQueryValidator : AbstractValidator<GetOrderByIdQuery>
	{
		public GetOrderByIdQueryValidator()
		{
			RuleFor(w => w.OrderId)
				.NotEmpty()
				.Must(w => ObjectId.TryParse(w, out var objectId));
		}
	}
}
