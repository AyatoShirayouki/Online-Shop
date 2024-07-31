using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain.Entities
{
	/// <summary>
	/// Represents a user's shopping cart containing multiple items.
	/// </summary>
	public class ShoppingCart : EntityBase
	{
		public Guid UserId { get; set; }
		public List<CartItem> Items { get; set; } = new List<CartItem>();
	}
}
