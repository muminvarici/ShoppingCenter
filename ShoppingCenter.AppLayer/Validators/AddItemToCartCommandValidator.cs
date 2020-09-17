using FluentValidation;
using ShoppingCenter.AppLayer.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.AppLayer.Validators
{
	public class AddItemToCartCommandValidator : AbstractValidator<AddItemToCartCommand>
	{
		public AddItemToCartCommandValidator()
		{
			RuleFor(w => w.ProductInfo)
				.NotNull()
				.Must(w => w.Quantity > 0);
			RuleFor(w => w.UserId)
				.NotEmpty();
		}
	}
}
