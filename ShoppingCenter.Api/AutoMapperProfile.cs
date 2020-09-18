using AutoMapper;
using MongoDB.Bson;
using ShoppingCenter.AppLayer.Commands.Products;
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
			CreateMap<AddProductCommand, Product>();
			CreateMap<UpdateProductCommand, Product>()
				.ForMember(w => w.Id, w => w.MapFrom(q => ObjectId.Parse(q.Id)));
		}
	}
}
