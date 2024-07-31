using MongoDB.Driver;
using OnlineShop.Application.Common.Interfaces;
using OnlineShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Infrastructure.Persistance.MongoDb
{
	/// <summary>
	/// Implements the shopping cart repository using MongoDB.
	/// </summary>
	public class MongoDbShoppingCartRepository : MongoDbRepository<ShoppingCart>, IShoppingCartRepository
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MongoDbShoppingCartRepository"/> class.
		/// </summary>
		/// <param name="context">The MongoDB context for database access.</param>
		public MongoDbShoppingCartRepository(MongoDbContext context) : base(context)
		{
		}

		public async Task<ShoppingCart> GetByUserId(Guid userId)
		{
			return await this.collection.Find(x => x.UserId == userId).FirstOrDefaultAsync();
		}
	}
}
