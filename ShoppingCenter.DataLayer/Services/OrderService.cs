using Core.DependencyInjection.Attributes;
using Core.DependencyInjection.Exceptions;
using ShoppingCenter.DataLayer.Models;
using ShoppingCenter.InfraStructure.Implementations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCenter.DataLayer.Services
{
	[ScopedDependency()]
	public class OrderService
	{
		private readonly IRepository<Order> orderRepository;
		private readonly ICartService cartService;
		private readonly IProductService productService;

		public OrderService(IRepository<Order> orderRepository, ICartService cartService, IProductService productService)
		{
			this.orderRepository = orderRepository;
			this.cartService = cartService;
			this.productService = productService;
		}

		public async Task<Order> GetOrderAsync(string id)
		{
			return await orderRepository.FindByIdAsync(id);
		}

		public async Task<Order> CreateOrderAsync(Order order)
		{
			var cart = await cartService.GetByUserAsync(order.UserId, false);
			if (order == null)
			{
				throw new ServiceException("Cart not found!");
			}
			if (order.PaymentDetail.Amount != cart.TotalPrice)
			{
				throw new ServiceException("Amount is not valid!");
			}
			var productIds = cart.Items.Select(w => w.Id);
			var products = (await productService.GetByIdsAsync(productIds)).ToList();
			foreach (var item in cart.Items)
			{
				var product = products.FirstOrDefault(w => w.Id == item.Id);
				if (product.Quantity < item.Quantity)
				{
					throw new ServiceException($"Product quantity is not enough, please refresh your cart");
				}
			}
			order.CartId = cart.Id.ToString();

			await orderRepository.InsertOneAsync(order);

			await UpdateProductQuantities(cart, products);

			await cartService.CheckoutCartAsync(cart.Id);

			return order;
		}

		private async Task UpdateProductQuantities(Cart cart, List<Product> products)
		{
			foreach (var item in cart.Items)
			{
				var product = products.FirstOrDefault(w => w.Id == item.Id);
				product.Quantity -= item.Quantity;
				await productService.SaveProductAsync(product.Id.ToString(), product);
			}
		}
	}
}
