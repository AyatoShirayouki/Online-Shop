using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.ShoppingCarts.Queries.GetShoppingCart
{
	/// <summary>
	/// Represents a model for items in a shopping cart.
	/// </summary>
	public class CartItemModel
	{
		public Guid ProductId { get; set; }
		public string? ProductName { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
		public decimal TotalPrice { get; set; }
	}
}
