using AutoMapper;
using OnlineShop.Application.ShoppingCarts.Queries.GetShoppingCart;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Application.Common.Mappings;

/// <summary>
/// Provides the mapping configuration for the application, using AutoMapper.
/// </summary>
public class MappingProfile : Profile
{
	/// <summary>
	/// Initializes a new instance of the <see cref="MappingProfile"/> class.
	/// </summary>
	public MappingProfile()
    {
        CreateMap<Product, Products.Queries.GetAllProducts.ProductModel>();
        CreateMap<Product, Products.Queries.GetSingleProduct.ProductModel>();

		CreateMap<ShoppingCart, ShoppingCartModel>();

		CreateMap<CartItem, CartItemModel>();
		
		
	}
}
