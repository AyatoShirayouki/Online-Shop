using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.ShoppingCarts.Queries.GetShoppingCart
{
	/// <summary>
	/// Represents a model for a shopping cart, including items and discounts.
	/// </summary>
	public class ShoppingCartModel
	{
		public List<CartItemModel> Items { get; set; } = new List<CartItemModel>();
		public decimal Discount { get; set; }
		public string? DiscountDescription { get; set; }
	}
}
