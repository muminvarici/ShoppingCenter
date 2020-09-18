using MediatR;
using ShoppingCenter.AppLayer.Models;

namespace ShoppingCenter.AppLayer.Queries
{
	public class GetCartByUserIdQuery : IRequest<CartResponse>
	{
		public string UserId { get; set; }
	}
}
