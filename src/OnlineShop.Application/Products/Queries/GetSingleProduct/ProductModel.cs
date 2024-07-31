namespace OnlineShop.Application.Products.Queries.GetSingleProduct;

/// <summary>
/// Represents a detailed product model used in queries.
/// </summary>
public class ProductModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
}
