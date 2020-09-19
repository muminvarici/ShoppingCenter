using MediatR;
using ShoppingCenter.AppLayer.Models;

namespace ShoppingCenter.AppLayer.Queries
{
	public class GetProductByIdQuery : IRequest<ProductResponse>
	{
		public string Id { get; set; }
	}
}
