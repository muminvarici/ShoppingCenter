using MediatR;
using ShoppingCenter.AppLayer.Models;

namespace ShoppingCenter.AppLayer.Queries
{
	public class GetOrderByIdQuery : IRequest<OrderResponse>
	{
		public string OrderId { get; set; }
	}
}
