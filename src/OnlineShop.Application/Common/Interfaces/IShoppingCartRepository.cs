using OnlineShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Common.Interfaces
{
	/// <summary>
	/// Defines the contract for a repository specifically for handling shopping cart operations.
	/// </summary>
	public interface IShoppingCartRepository : IRepository<ShoppingCart>
	{
		/// <summary>
		/// Retrieves the shopping cart associated with a specific user.
		/// </summary>
		/// <param name="userId">The unique identifier of the user.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the user's shopping cart.</returns>
		Task<ShoppingCart> GetByUserId(Guid userId);
	}
}
