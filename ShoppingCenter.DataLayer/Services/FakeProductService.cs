using Bogus;
using Core.DependencyInjection.Attributes;
using MongoDB.Bson;
using ShoppingCenter.DataLayer.Models;
using ShoppingCenter.InfraStructure.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCenter.DataLayer.Services
{
	[ScopedDependency(ServiceType = typeof(IProductService), DevelopmentOnly = true)]
	public class FakeProductService : ProductServiceBase, IProductService
	{
		public FakeProductService(IRepository<Product> repository) : base(repository)
		{
		}
		public async Task InitializeDataAsync()
		{
			//repository.DeleteMany(w => true);
			if (await Repository.CountAsync() > 100)
			{
				return;
			}

			var startDate = DateTime.UtcNow.AddYears(-1);
			var endDate = DateTime.UtcNow.AddMonths(-1);
			var productFaker = new Faker<Product>()
				.RuleFor(w => w.Name, w => w.Commerce.ProductName())
				.RuleFor(w => w.Quantity, w => w.Commerce.Random.Int(1, 1000))
				.RuleFor(w => w.Price, w => Convert.ToDecimal(w.Commerce.Price(5, 100, 2)));
			var data = productFaker.GenerateLazy(100).ToList();
			await Repository.InsertManyAsync(data);
			await Repository.InsertOneAsync(new Product
			{
				Id = ObjectId.Parse("111111111111111111111111"),
				CreatedAt = new DateTime(1991, 04, 12),
				Name = "Manuel test product",
				Price = 1111,
				Quantity = 1,
			}, false);
		}
	}
}
