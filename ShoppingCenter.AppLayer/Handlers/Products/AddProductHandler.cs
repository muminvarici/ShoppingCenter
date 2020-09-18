using AutoMapper;
using MediatR;
using ShoppingCenter.AppLayer.Commands.Products;
using ShoppingCenter.AppLayer.Models;
using ShoppingCenter.DataLayer.Models;
using ShoppingCenter.DataLayer.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingCenter.AppLayer.Handlers.Products
{
	public class AddProductHandler : IRequestHandler<AddProductCommand, ProductResponse>
	{
		private readonly IMapper mapper;
		private readonly IProductService productService;

		public AddProductHandler(IMapper mapper, IProductService productService)
		{
			this.mapper = mapper;
			this.productService = productService;
		}

		public async Task<ProductResponse> Handle(AddProductCommand request, CancellationToken cancellationToken)
		{
			var requestedProduct = mapper.Map<Product>(request);
			var result = await productService.SaveProductAsync(string.Empty, requestedProduct);
			return mapper.Map<ProductResponse>(result);
		}
	}
}
