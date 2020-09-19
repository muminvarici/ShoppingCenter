using FluentValidation;
using ShoppingCenter.AppLayer.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.AppLayer.Validators
{
	public class GetProductByIdQueryValidator:AbstractValidator<GetProductByIdQuery>
	{
		public GetProductByIdQueryValidator()
		{
			RuleFor(w=>w.Id)
				.NotEmpty()
				.Must(id =>
				{
					return MongoDB.Bson.ObjectId.TryParse(id, out var objectId);
				});
		}
	}
}
