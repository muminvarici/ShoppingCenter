using ShoppingCenter.DataLayer.Models;
using System;
using System.Threading.Tasks;

namespace ShoppingCenter.DataLayer.Services
{
	public interface ICartService
	{
		Task<Cart> GetByIdAsync(string id);
		Task<Cart> AddItemToCartAsync(Cart cart, Product product, int quantity, string userId);
		Task<Cart> GetByUserAsync(string userId);
	}
}