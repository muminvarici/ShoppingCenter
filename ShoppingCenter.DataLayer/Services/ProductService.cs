using Core.DependencyInjection.Attributes;
using ShoppingCenter.DataLayer.Models;
using ShoppingCenter.InfraStructure;
using ShoppingCenter.InfraStructure.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCenter.DataLayer.Services
{
	[ScopedDependency(ServiceType = typeof(IProductService), ProductionOnly = true)]
	public class ProductService : ProductServiceBase, IProductService
	{
		public ProductService(IRepository<Product> repository) : base(repository)
		{
		}
	}
}
