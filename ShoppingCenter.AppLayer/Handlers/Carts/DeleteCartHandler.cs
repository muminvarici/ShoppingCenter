using MediatR;
using ShoppingCenter.AppLayer.Commands.Carts;
using ShoppingCenter.DataLayer.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingCenter.AppLayer.Handlers.Carts
{
	public class DeleteCartHandler : IRequestHandler<DeleteCartCommand>
	{
		private readonly ICartService cartService;

		public DeleteCartHandler(ICartService cartService)
		{
			this.cartService = cartService;
		}
		public async Task<Unit> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
		{
			await cartService.DeleteAsync(request.Id);
			return Unit.Value;
		}
	}
}
