using OnlineShop.Domain.Entities;

namespace OnlineShop.Application.Common.Interfaces;

/// <summary>
/// Defines the contract for a read-only repository, providing methods to retrieve entities from a data source.
/// </summary>
/// <typeparam name="TEntity">The type of entity managed by the repository.</typeparam>
public interface IReadRepository<TEntity> where TEntity : EntityBase
{
	/// <summary>
	/// Retrieves an entity by its unique identifier.
	/// </summary>
	/// <param name="id">The unique identifier of the entity.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the entity.</returns>
	Task<TEntity> Get(Guid id);

	/// <summary>
	/// Retrieves all entities.
	/// </summary>
	/// <returns>A task that represents the asynchronous operation. The task result contains a collection of entities.</returns>
	Task<IEnumerable<TEntity>> Get();

	/// <summary>
	/// Retrieves entities that satisfy a specific specification.
	/// </summary>
	/// <param name="specification">The specification to filter the entities.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains a collection of entities.</returns>
	Task<IEnumerable<TEntity>> Get(ISpecification<TEntity> specification);


	/// <summary>
	/// Retrieves entities by their unique identifiers.
	/// </summary>
	/// <param name="ids">A collection of unique identifiers of the entities.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains a collection of entities.</returns>
	Task<IEnumerable<TEntity>> Get(IEnumerable<Guid> ids);

	/// <summary>
	/// Counts the total number of entities.
	/// </summary>
	/// <returns>A task that represents the asynchronous operation. The task result contains the number of entities.</returns>
	Task<long> Count();
}
