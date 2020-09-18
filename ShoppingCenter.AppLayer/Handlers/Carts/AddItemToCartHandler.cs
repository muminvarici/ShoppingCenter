using AutoMapper;
using MediatR;
using ShoppingCenter.AppLayer.Commands.Carts;
using ShoppingCenter.AppLayer.Models;
using ShoppingCenter.DataLayer.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingCenter.AppLayer.Handlers.Carts
{
	public class AddItemToCartHandler : IRequestHandler<AddItemToCartCommand, CartResponse>
	{
		private readonly ICartService cartService;
		private readonly IMapper mapper;
		private readonly IProductService productService;

		public AddItemToCartHandler(ICartService cartService, IMapper mapper, IProductService productService)
		{
			this.cartService = cartService;
			this.mapper = mapper;
			this.productService = productService;
		}
		public async Task<CartResponse> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
		{
			//var cart = await cartService.GetByIdAsync(request.Id);
			var cart = await cartService.GetByUserAsync(request.UserId);

			var product = await productService.GetByIdAsync(request.ProductInfo.Id);

			cart = await cartService.AddItemToCartAsync(cart, product, request.ProductInfo.Quantity, request.UserId);
			cart = await cartService.GetByIdAsync(cart.Id.ToString());
			return mapper.Map<CartResponse>(cart);
		}
	}
}
