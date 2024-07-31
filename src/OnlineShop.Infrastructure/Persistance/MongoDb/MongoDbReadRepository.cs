using MongoDB.Driver;
using OnlineShop.Application.Common.Interfaces;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Infrastructure.Persistance.MongoDb;

/// <summary>
/// Implements the read-only repository interface using MongoDB.
/// </summary>
/// <typeparam name="TEntity">The type of entity managed by the repository.</typeparam>
public class MongoDbReadRepository<TEntity> : IReadRepository<TEntity> where TEntity : EntityBase
{
    protected readonly IMongoCollection<TEntity> collection;

	/// <summary>
	/// Initializes a new instance of the <see cref="MongoDbReadRepository{TEntity}"/> class.
	/// </summary>
	/// <param name="context">The MongoDB context for database access.</param>
	public MongoDbReadRepository(MongoDbContext context)
    {
        this.collection = context.Database.GetCollection<TEntity>(typeof(TEntity).Name);
    }

	public async Task<TEntity> Get(Guid id)
    {
        return await this.collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TEntity>> Get()
    {
        return await this.collection.Find(_ => true).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> Get(ISpecification<TEntity> specification)
    {
        return await this.collection.Find(specification.Filter).SortBy(specification.SortBy).ToListAsync();
    }

	public async Task<IEnumerable<TEntity>> Get(IEnumerable<Guid> ids)
	{
		return await this.collection.Find(x => ids.Contains(x.Id)).ToListAsync();
	}

	public async Task<long> Count()
    {
        return await this.collection.CountDocumentsAsync(x => true);
    }
}
