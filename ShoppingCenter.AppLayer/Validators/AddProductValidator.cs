using FluentValidation;
using ShoppingCenter.AppLayer.Commands.Products;

namespace ShoppingCenter.AppLayer.Validators
{
	public class AddProductValidator : AbstractValidator<AddProductCommand>
	{
		public AddProductValidator()
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
