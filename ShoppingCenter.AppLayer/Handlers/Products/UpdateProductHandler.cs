using AutoMapper;
using MediatR;
using ShoppingCenter.AppLayer.Commands.Products;
using ShoppingCenter.AppLayer.Models;
using ShoppingCenter.DataLayer.Models;
using ShoppingCenter.DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingCenter.AppLayer.Handlers.Products
{
	public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ProductResponse>
	{
		private readonly IProductService productService;
		private readonly IMapper mapper;

		public UpdateProductHandler(IProductService productService, IMapper mapper)
		{
			this.productService = productService;
			this.mapper = mapper;
		}
		public async Task<ProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
		{
			var requestedProduct = mapper.Map<Product>(request);
			var result = await productService.SaveProductAsync(request.Id, requestedProduct);
			return mapper.Map<ProductResponse>(result);
		}
	}
}
