using Core.DependencyInjection.Attributes;
using MongoDB.Bson;
using ShoppingCenter.DataLayer.Models;
using ShoppingCenter.InfraStructure.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
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

		public async Task<Cart> AddItemToCartAsync(Cart cart, Product product, int quantity, string userId)
		{
			if (cart == null)
			{
				cart = new Cart
				{
					UserId = userId,
					Items = new List<Product> { GetMinifiedProduct(product, quantity) }
				};
				await cartRepository.InsertOneAsync(cart);
			}
			else
			{
				var item = cart.Items.FirstOrDefault(w => w.Id == product.Id);
				if (item == null)
				{
					cart.Items.Add(GetMinifiedProduct(product, quantity));
				}
				else
				{
					item.Quantity += quantity;
				}
				await cartRepository.ReplaceOneAsync(cart);
			}
			return cart;
		}

		private Product GetMinifiedProduct(Product product, int quantity)
		{
			return new Product { Id = product.Id, Quantity = quantity };
		}

		public async Task<Cart> GetByIdAsync(string id)
		{
			if (!MongoDB.Bson.ObjectId.TryParse(id, out var value))
			{
				return null;
			}
			return await cartRepository.FindByIdAsync(id);
		}

		public async Task<Cart> GetByUserAsync(string userId)
		{
			return await cartRepository.FindOneAsync(w => w.UserId == userId);
		}
	}
}
