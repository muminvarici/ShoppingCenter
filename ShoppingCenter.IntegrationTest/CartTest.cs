using FluentAssertions;
using MongoDB.Bson;
using Newtonsoft.Json;
using ShoppingCenter.Api;
using ShoppingCenter.AppLayer.Commands.Carts;
using ShoppingCenter.AppLayer.Commands.Products;
using ShoppingCenter.AppLayer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ShoppingCenter.IntegrationTest
{
	public class CartTest : IClassFixture<TestFixture<Startup>>
	{
		private const string testProductId = "111111111111111111111111";
		private const string testUserId = "testUserId";
		private HttpClient client;

		public CartTest(TestFixture<Startup> fixture)
		{
			client = fixture.Client;
		}

		[Fact]
		public async Task TestPostCartAsync()
		{
			var cart = await CreateUserCart(1);
			ValidateCart(cart, testUserId, testProductId, 1);

			var userCart = await GetUserCartAsync();
			ValidateCart(userCart, testUserId, testProductId, 1);
		}

		private async Task<CartResponse> GetUserCartAsync()
		{
			// Arrange
			var request = $"/api/Cart/GetByUserId/{testUserId}";

			// Act
			var response = await client.GetAsync(request);

			// Assert
			response.EnsureSuccessStatusCode();

			var cart = JsonConvert.DeserializeObject<CartResponse>(await response.Content.ReadAsStringAsync());
			return cart;
		}

		private async Task<CartResponse> CreateUserCart(int quantity)
		{
			// Arrange
			var request = new
			{
				Url = "/api/Cart",
				Body = new AddItemToCartCommand
				{
					ProductInfo = new ProductRequest
					{
						Id = testProductId,
						Quantity = quantity
					},
					UserId = testUserId
				}
			};

			// Act
			var response = await client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

			// Assert
			response.EnsureSuccessStatusCode();
			CartResponse cart = await GetCartFromResponseAsync(response);
			return cart;
		}

		private void ValidateCart(CartResponse cart, string userId, string productId, int quantity)
		{
			cart.Should().NotBeNull();
			cart.UserId.Should().BeEquivalentTo(testUserId);
			cart.Items.Should().NotBeNullOrEmpty();
			cart.TotalPrice.Should().BeGreaterThan(0);
			var product = cart.Items.FirstOrDefault(w => w.Id == ObjectId.Parse(productId));
			product.Should().NotBeNull();
			product.Quantity.Should().BeGreaterOrEqualTo(quantity);
		}

		private static async Task<CartResponse> GetCartFromResponseAsync(HttpResponseMessage response)
		{
			return JsonConvert.DeserializeObject<CartResponse>(await response.Content.ReadAsStringAsync());
		}
	}
}