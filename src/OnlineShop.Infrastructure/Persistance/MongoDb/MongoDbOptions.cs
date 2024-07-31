namespace OnlineShop.Infrastructure.Persistance.MongoDb;

/// <summary>
/// Represents configuration options for connecting to MongoDB.
/// </summary>
public class MongoDbOptions
{
    public string? ConnectionString { get; set; }
    public string? DatabaseName { get; set; }
}
