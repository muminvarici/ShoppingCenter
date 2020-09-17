using Core.DependencyInjection.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.DataLayer.Services
{
	[SingletonDependency(ServiceType = typeof(IProductService), DevelopmentOnly = true)]
	public class FakeProductService : IProductService
	{
	}
}
