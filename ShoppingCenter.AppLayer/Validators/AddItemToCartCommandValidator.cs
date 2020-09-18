using FluentValidation;
using ShoppingCenter.AppLayer.Commands.Carts;

namespace ShoppingCenter.AppLayer.Validators
{
	public class AddItemToCartCommandValidator : AbstractValidator<AddItemToCartCommand>
	{
		public AddItemToCartCommandValidator()
		{
			RuleFor(w => w.ProductInfo)
				.NotNull();
			RuleFor(w => w.ProductInfo.Quantity)
				.NotEmpty();
			RuleFor(w => w.UserId)
				.NotEmpty();
		}
	}
}
