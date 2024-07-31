using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain.Entities
{
	/// <summary>
	/// Represents an item in a shopping cart.
	/// </summary>
	public class CartItem : EntityBase
	{
		public Guid ProductId { get; set; }
		public int Quantity { get; set; }
	}
}
