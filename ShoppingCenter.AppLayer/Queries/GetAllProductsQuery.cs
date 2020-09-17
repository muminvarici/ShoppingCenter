using MediatR;
using ShoppingCenter.AppLayer.Models;
using System.Collections.Generic;

namespace ShoppingCenter.AppLayer.Queries
{
	public class GetAllProductsQuery : IRequest<IEnumerable<ProductResponse>>
	{

	}
}
