using Core.DependencyInjection.Attributes;
using ShoppingCenter.DataLayer.Models;
using ShoppingCenter.InfraStructure.Implementations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCenter.DataLayer.Services
{
	[ScopedDependency(ServiceType = typeof(ICartService))]
	public class CartService : ICartService
	{
		private readonly IMongoRepository<Cart> cartRepository;

		public CartService(IMongoRepository<Cart> cartRepository)
		{
			this.cartRepository = cartRepository;
		}
		public async Task<Cart> GetByIdAsync(string id)
		{
			if (!MongoDB.Bson.ObjectId.TryParse(id, out var value))
			{
				return null;
			}
			return await cartRepository.FindByIdAsync(id);
		}
	}
}
