using FluentValidation;
using ShoppingCenter.AppLayer.Commands.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.AppLayer.Validators
{
	public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
	{
		public CreateOrderCommandValidator()
		{
			RuleFor(w => w.Address)
				.NotNull();
			RuleFor(w => w.UserId)
				.NotEmpty();
			RuleFor(w => w.PaymentDetail)
				.NotNull();
		}
	}
}
