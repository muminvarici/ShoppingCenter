using FluentAssertions;
using Newtonsoft.Json;
using ShoppingCenter.Api;
using ShoppingCenter.AppLayer.Commands.Products;
using ShoppingCenter.AppLayer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ShoppingCenter.IntegrationTest
{
	public class ProductTest : IClassFixture<TestFixture<Startup>>
	{
		private const string testId = "111111111111111111111111";
		private HttpClient client;

		public ProductTest(TestFixture<Startup> fixture)
		{
			client = fixture.Client;
		}


		[Fact]
		public async Task TestGetProductAsync()
		{
			ProductResponse product = await GetTestProduct();

			product.Should().NotBeNull();
			product.Id.Should().Be(testId);
		}

		private async Task<ProductResponse> GetTestProduct()
		{
			// Arrange
			var request = $"/api/Product/{testId}";

			// Act
			var response = await client.GetAsync(request);

			// Assert
			response.EnsureSuccessStatusCode();

			ProductResponse product = await GetProductFromResponseAsync(response);

			return product;
		}

		[Fact]
		public async Task TestGetProductsAsync()
		{
			// Arrange
			var request = "/api/Product";

			// Act
			var response = await client.GetAsync(request);

			// Assert
			response.EnsureSuccessStatusCode();

			var products = JsonConvert.DeserializeObject<IEnumerable<ProductResponse>>(await response.Content.ReadAsStringAsync());
			products.Should().NotBeNullOrEmpty();
			products.Should().Contain(w => w.Id.Equals(testId));
		}

		[Fact]
		public async Task TestPostProductAsync()
		{
			// Arrange
			var request = new
			{
				Url = "/api/Product",
				Body = new AddProductCommand
				{
					Name = "armut",
					Price = 222,
					Quantity = 2
				}
			};

			// Act
			var response = await client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

			// Assert
			response.EnsureSuccessStatusCode();
			ProductResponse product = await GetProductFromResponseAsync(response);

			ValidateProduct(product, request.Body.Quantity, request.Body.Price, request.Body.Name);
		}

		private void ValidateProduct(ProductResponse product, int quantity, decimal price, string name)
		{
			product.Should().NotBeNull();
			product.Id.Should().NotBeNullOrWhiteSpace();
			product.Quantity.Should().Be(quantity);
			product.Price.Should().Be(price);
			product.Name.Should().BeEquivalentTo(name);
		}

		private static async Task<ProductResponse> GetProductFromResponseAsync(HttpResponseMessage response)
		{
			return JsonConvert.DeserializeObject<ProductResponse>(await response.Content.ReadAsStringAsync());
		}

		[Fact]
		public async Task TestPutProductAsync()
		{
			// Arrange
			var request = new
			{
				Url = "/api/Product/",
				Body = new UpdateProductCommand
				{
					Id = testId,
					Name = "Changed Manuel test product",
					Price = 123123,
					Quantity = 2
				}
			};

			// Act
			var response = await client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));

			// Assert
			response.EnsureSuccessStatusCode();

			ProductResponse product = await GetProductFromResponseAsync(response);

			ValidateProduct(product, request.Body.Quantity, request.Body.Price, request.Body.Name);
		}
	}
}