using AutoMapper;
using ShoppingCenter.AppLayer.Models;
using ShoppingCenter.DataLayer.Models;

namespace ShoppingCenter.Api
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Product, ProductResponse>();
			CreateMap<ProductRequest, Product>();
			CreateMap<Cart, CartResponse>();
		}
	}
}
