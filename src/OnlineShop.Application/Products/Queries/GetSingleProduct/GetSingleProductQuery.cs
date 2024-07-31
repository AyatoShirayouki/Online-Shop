using MediatR;

namespace OnlineShop.Application.Products.Queries.GetSingleProduct;

/// <summary>
/// Query to retrieve a single product by its identifier.
/// </summary>
public class GetSingleProductQuery : IRequest<ProductModel>
{
    public Guid Id { get; set; }
}
