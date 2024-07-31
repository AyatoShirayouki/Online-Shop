using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.ShoppingCarts.Queries.RemoveFromCart
{
	/// <summary>
	/// Query to remove an item from the user's shopping cart.
	/// </summary>
	public class RemoveFromCartQuery : IRequest
	{
		public Guid UserId { get; set; }
		public Guid ProductId { get; set; }
	}
}
