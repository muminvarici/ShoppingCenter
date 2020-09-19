using FluentValidation;
using ShoppingCenter.AppLayer.Commands.Products;

namespace ShoppingCenter.AppLayer.Validators
{
	public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
	{
		public AddProductCommandValidator()
		{
			RuleFor(w => w.Name)
				.NotEmpty();
			RuleFor(w => w.Price)
				.NotEmpty();
			RuleFor(w => w.Quantity)
				.NotEmpty();
		}
	}
}
