using Bogus;
using Core.DependencyInjection.Attributes;
using ShoppingCenter.DataLayer.Models;
using ShoppingCenter.InfraStructure.Implementations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCenter.DataLayer.Services
{
	[ScopedDependency(ServiceType = typeof(IProductService), DevelopmentOnly = true)]
	public class FakeProductService : IProductService
	{
		private readonly IMongoRepository<Product> repository;

		public FakeProductService(IMongoRepository<Product> repository)
		{
			this.repository = repository;
		}

		public async Task InitializeDataAsync()
		{
			repository.DeleteMany(w => true);
			var startDate = DateTime.UtcNow.AddYears(-1);
			var endDate = DateTime.UtcNow.AddMonths(-1);
			var productFaker = new Faker<Product>()
				.RuleFor(w => w.Name, w => w.Commerce.ProductName())
				.RuleFor(w => w.Quantity, w => w.Commerce.Random.Int(1, 1000))
				.RuleFor(w => w.Price, w => Convert.ToDecimal(w.Commerce.Price(5, 100, 2)));
			var data = productFaker.GenerateLazy(100).ToList();
			await repository.InsertManyAsync(data);
		}
	}
}
