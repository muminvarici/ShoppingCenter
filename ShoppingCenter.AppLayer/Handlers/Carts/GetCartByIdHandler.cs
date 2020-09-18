using AutoMapper;
using MediatR;
using ShoppingCenter.AppLayer.Models;
using ShoppingCenter.AppLayer.Queries;
using ShoppingCenter.DataLayer.Models;
using ShoppingCenter.DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingCenter.AppLayer.Handlers.Carts
{
	public class GetCartByIdHandler : IRequestHandler<GetCartByIdQuery, CartResponse>
	{
		private readonly ICartService cartService;
		private readonly IMapper mapper;

		public GetCartByIdHandler(ICartService cartService, IMapper mapper)
		{
			this.cartService = cartService;
			this.mapper = mapper;
		}
		public async Task<CartResponse> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
		{
			var result = await cartService.GetByIdAsync(request.Id);
			return mapper.Map<CartResponse>(result);
		}
	}
}
