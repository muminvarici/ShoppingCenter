using AutoMapper;
using MediatR;
using ShoppingCenter.AppLayer.Models;
using ShoppingCenter.AppLayer.Queries;
using ShoppingCenter.DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingCenter.AppLayer.Handlers.Products
{
	public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductResponse>>
	{
		private readonly IProductService productService;
		private readonly IMapper mapper;

		public GetAllProductsHandler(IProductService productService,IMapper mapper)
		{
			this.productService = productService;
			this.mapper = mapper;
		}
		public async Task<IEnumerable<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
		{
			var result = await productService.GetAllAsync();
			return result.Select(w => mapper.Map<ProductResponse>(w));
		}
	}
}
