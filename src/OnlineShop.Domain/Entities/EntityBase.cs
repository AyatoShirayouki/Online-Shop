namespace OnlineShop.Domain.Entities;

/// <summary>
/// Base class for all entities in the domain, providing a unique identifier.
/// </summary>
public class EntityBase
{
    public Guid Id { get; set; }
}
