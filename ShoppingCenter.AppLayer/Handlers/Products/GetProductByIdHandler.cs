using AutoMapper;
using MediatR;
using ShoppingCenter.AppLayer.Models;
using ShoppingCenter.AppLayer.Queries;
using ShoppingCenter.DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingCenter.AppLayer.Handlers.Products
{
	public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
	{
		private readonly IProductService productService;
		private readonly IMapper mapper;

		public GetProductByIdHandler(IProductService productService, IMapper mapper)
		{
			this.productService = productService;
			this.mapper = mapper;
		}
		public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
		{
			return mapper.Map<ProductResponse>(await productService.GetByIdAsync(request.Id));
		}
	}
}
