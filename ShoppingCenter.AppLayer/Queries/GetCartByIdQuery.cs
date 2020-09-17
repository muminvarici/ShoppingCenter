using MediatR;
using ShoppingCenter.AppLayer.Models;

namespace ShoppingCenter.AppLayer.Queries
{
	public class GetCartByIdQuery : IRequest<CartResponse>
	{
		public string Id { get; set; }
	}
}
