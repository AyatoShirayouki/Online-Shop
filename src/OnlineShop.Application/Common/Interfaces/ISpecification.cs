using System.Linq.Expressions;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Application.Common.Interfaces;

/// <summary>
/// Defines the contract for a specification used to query entities from a repository.
/// </summary>
/// <typeparam name="TEntity">The type of entity managed by the specification.</typeparam>
public interface ISpecification<TEntity> where TEntity : EntityBase
{
	/// <summary>
	/// Gets the expression used to filter entities.
	/// </summary>
	Expression<Func<TEntity, bool>> Filter { get; }

	/// <summary>
	/// Gets the expression used to sort entities.
	/// </summary>
	Expression<Func<TEntity, object>> SortBy{ get; }
}
