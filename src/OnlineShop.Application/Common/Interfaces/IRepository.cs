using System;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Application.Common.Interfaces;

/// <summary>
/// Defines the contract for a repository with read and write operations.
/// </summary>
/// <typeparam name="TEntity">The type of entity managed by the repository.</typeparam>
public interface IRepository<TEntity> : IReadRepository<TEntity> where TEntity : EntityBase
{
	/// <summary>
	/// Adds a new entity to the repository.
	/// </summary>
	/// <param name="entity">The entity to add.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the added entity.</returns>
	Task<TEntity> Add(TEntity entity);

	/// <summary>
	/// Updates an existing entity in the repository.
	/// </summary>
	/// <param name="entity">The entity to update.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	Task Update(TEntity entity);

	/// <summary>
	/// Deletes an entity from the repository.
	/// </summary>
	/// <param name="entity">The entity to delete.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	Task Delete(TEntity entity);
}

