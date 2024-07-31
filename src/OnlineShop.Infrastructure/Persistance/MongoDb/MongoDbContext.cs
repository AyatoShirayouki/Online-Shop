using System;
using MongoDB.Driver;

namespace OnlineShop.Infrastructure.Persistance.MongoDb;

/// <summary>
/// Represents the MongoDB context for accessing the database.
/// </summary>
public class MongoDbContext
{
    public IMongoDatabase Database { get; init; }

	/// <summary>
	/// Initializes a new instance of the <see cref="MongoDbContext"/> class.
	/// </summary>
	/// <param name="options">The options for configuring the MongoDB context.</param>
	public MongoDbContext(MongoDbOptions options)
    {
        IMongoClient mongoClient = new MongoClient(options.ConnectionString);
        this.Database = mongoClient.GetDatabase(options.DatabaseName);
    }
}

