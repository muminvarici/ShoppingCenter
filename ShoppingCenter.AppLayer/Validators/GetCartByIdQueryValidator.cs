using FluentValidation;
using FluentValidation.Results;
using ShoppingCenter.AppLayer.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.AppLayer.Validators
{
	public class GetCartByIdQueryValidator : AbstractValidator<GetCartByIdQuery>
	{
		public GetCartByIdQueryValidator()
		{
			RuleFor(w => w.Id)
				.NotEmpty()
				.Must(id =>
				{
					return MongoDB.Bson.ObjectId.TryParse(id, out var objectId);
				});
		}
	}
}
