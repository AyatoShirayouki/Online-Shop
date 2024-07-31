using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.ShoppingCarts.Queries.GetShoppingCart
{
	/// <summary>
	/// Query to retrieve the shopping cart for a specific user.
	/// </summary>
	public class GetShoppingCartQuery : IRequest<ShoppingCartModel>
	{
		public Guid UserId { get; set; }
	}
}
