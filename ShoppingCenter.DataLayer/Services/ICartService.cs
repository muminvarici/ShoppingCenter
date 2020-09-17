using ShoppingCenter.DataLayer.Models;
using System.Threading.Tasks;

namespace ShoppingCenter.DataLayer.Services
{
	public interface ICartService
	{
		Task<Cart> GetByIdAsync(string id);
	}
}