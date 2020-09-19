using Core.DependencyInjection.Attributes;
using Core.DependencyInjection.Exceptions;
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
		private readonly IRepository<Cart> cartRepository;
		private readonly IProductService productService;

		public CartService(IRepository<Cart> cartRepository, IProductService productService)
		{
			this.cartRepository = cartRepository;
			this.productService = productService;
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
				foreach (var cartItem in cart.Items)
				{
					ToMinifiedProduct(cartItem, cartItem.Quantity);
				}
				var item = cart.Items.FirstOrDefault(w => w.Id == product.Id);
				CheckProductCount(item, product.Quantity, quantity);
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

		private void CheckProductCount(Product item, int availableQuantity, int quantity)
		{
			if (item.Quantity + quantity > availableQuantity)
			{
				throw new ServiceException("Product quantity is not available");
			}
		}

		private Product GetMinifiedProduct(Product product, int quantity)
		{
			return new Product { Id = product.Id, Quantity = quantity };
		}

		private void ToMinifiedProduct(Product product, int quantity)
		{
			product.Price = 0;
			product.Name = null;
		}

		public async Task<Cart> GetByIdAsync(string id)
		{
			var cart = await cartRepository.FindByIdAsync(id);
			if (cart == null)
			{
				throw new ServiceException("Cart not found");
			}
			await FillCartDetailsAsync(cart);
			return cart;
		}

		private async Task FillCartDetailsAsync(Cart cart)
		{
			var products = (await productService.GetByIdsAsync(cart.Items.Select(w => w.Id))).ToList();
			foreach (var item in cart.Items)
			{
				var product = products.FirstOrDefault(w => w.Id == item.Id);
				FillProductProperties(item, product);
			}
			cart.TotalPrice = cart.Items.Sum(w => w.Quantity * w.Price);
		}

		private void FillProductProperties(Product target, Product source)
		{
			target.Name = source.Name;
			target.Price = source.Price;
		}

		public async Task<Cart> GetByUserAsync(string userId, bool includeCheckedOut)
		{
			var cart = await cartRepository.FindOneAsync(w => w.UserId == userId && (includeCheckedOut || !includeCheckedOut && !w.CheckedOut));
			if (cart != null)
			{
				await FillCartDetailsAsync(cart);
			}

			return cart;
		}

		public async Task DeleteAsync(string id)
		{
			if (!cartRepository.Contains(w => w.Id == ObjectId.Parse(id)))
			{
				throw new ServiceException("Cart not found");
			}
			await cartRepository.DeleteByIdAsync(id);
		}

		public async Task CheckoutCartAsync(ObjectId id)
		{
			var cart = await cartRepository.FindByIdAsync(id.ToString());
			cart.CheckedOut = true;
			await cartRepository.ReplaceOneAsync(cart);
		}
	}
}
