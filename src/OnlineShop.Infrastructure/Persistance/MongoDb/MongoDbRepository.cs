using MongoDB.Driver;
using OnlineShop.Application.Common.Interfaces;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Infrastructure.Persistance.MongoDb;

/// <summary>
/// Implements the repository interface for read and write operations using MongoDB.
/// </summary>
/// <typeparam name="TEntity">The type of entity managed by the repository.</typeparam>
public class MongoDbRepository<TEntity> : MongoDbReadRepository<TEntity>, IRepository<TEntity> where TEntity : EntityBase
{
	// <summary>
	/// Initializes a new instance of the <see cref="MongoDbRepository{TEntity}"/> class.
	/// </summary>
	/// <param name="context">The MongoDB context for database access.</param>
	public MongoDbRepository(MongoDbContext context) : base(context)
    {
    }

    public async Task<TEntity> Add(TEntity entity)
    {
        entity.Id = Guid.NewGuid();
        await this.collection.InsertOneAsync(entity);
        return await this.Get(entity.Id);
    }

    public async Task Update(TEntity entity)
    {
        await this.collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
    }

    public async Task Delete(TEntity entity)
    {
        await this.collection.DeleteOneAsync(x => x.Id == entity.Id);
    }
}
