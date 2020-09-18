using FluentValidation;
using MongoDB.Bson;
using ShoppingCenter.AppLayer.Commands.Carts;

namespace ShoppingCenter.AppLayer.Validators
{
	public class DeleteCartCommandValidator : AbstractValidator<DeleteCartCommand>
	{
		public DeleteCartCommandValidator()
		{
			RuleFor(w => w.Id)
				.NotEmpty()
				.Must(w => ObjectId.TryParse(w, out var objectId));
		}
	}
}
