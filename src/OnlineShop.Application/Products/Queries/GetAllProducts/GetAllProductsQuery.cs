using MediatR;

namespace OnlineShop.Application.Products.Queries.GetAllProducts;

/// <summary>
/// Query to retrieve all products in the store.
/// </summary>
public class GetAllProductsQuery : IRequest<IEnumerable<ProductModel>> {}
