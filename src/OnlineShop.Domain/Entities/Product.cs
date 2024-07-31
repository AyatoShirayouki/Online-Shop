using System;

namespace OnlineShop.Domain.Entities;

/// <summary>
/// Represents a product in the online shop.
/// </summary>
public class Product : EntityBase
{
	public string? Name { get; set; }
	public decimal Price { get; set; }
	public string? ImageUrl { get; set; }
	public string? Description { get; set; }
}
