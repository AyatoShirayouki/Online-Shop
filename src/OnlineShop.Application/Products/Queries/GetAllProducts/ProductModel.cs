namespace OnlineShop.Application.Products.Queries.GetAllProducts;

/// <summary>
/// Represents a simplified product model used in queries.
/// </summary>
public class ProductModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
}
