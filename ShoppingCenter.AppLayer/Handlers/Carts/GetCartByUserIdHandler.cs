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
	public class GetCartByUserIdHandler : IRequestHandler<GetCartByUserIdQuery, CartResponse>
	{
		private readonly ICartService cartService;
		private readonly IMapper mapper;

		public GetCartByUserIdHandler(ICartService cartService, IMapper mapper)
		{
			this.cartService = cartService;
			this.mapper = mapper;
		}
		public async Task<CartResponse> Handle(GetCartByUserIdQuery request, CancellationToken cancellationToken)
		{
			var result = await cartService.GetByUserAsync(request.UserId);
			return mapper.Map<CartResponse>(result);
		}
	}
}
