using MongoDB.Bson;
using ShoppingCenter.DataLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingCenter.DataLayer.Services
{
	public interface IProductService
	{
		Task<IEnumerable<Product>> GetAllAsync();
		Task<Product> GetByIdAsync(string id);
		Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<ObjectId> ids);
	}
}