using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.ShoppingCarts.Queries.AddToCart
{
	/// <summary>
	/// Query to add an item to the user's shopping cart.
	/// </summary>
	public class AddToCartQuery : IRequest
	{
		public Guid UserId { get; set; }
		public Guid ProductId { get; set; }
		public int Quantity { get; set; }
	}
}
