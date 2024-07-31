using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.ShoppingCarts.Queries.AddToCart;
using OnlineShop.Application.ShoppingCarts.Queries.GetShoppingCart;
using OnlineShop.Application.ShoppingCarts.Queries.RemoveFromCart;
using System;
using System.Threading.Tasks;

namespace OnlineShop.WebApi.Controllers
{
	/// <summary>
	/// API Controller for managing shopping cart-related operations.
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class ShoppingCartController : ControllerBase
	{
		private readonly IMediator mediator;

		/// <summary>
		/// Initializes a new instance of the <see cref="ShoppingCartController"/> class.
		/// </summary>
		/// <param name="mediator">The mediator for handling requests.</param>
		public ShoppingCartController(IMediator mediator)
		{
			this.mediator = mediator;
		}

		/// <summary>
		/// Adds an item to the user's shopping cart.
		/// </summary>
		/// <param name="query">The query containing the item details.</param>
		/// <returns>An action result indicating the outcome.</returns>
		[HttpPost("add")]
		public async Task<IActionResult> AddToCart(AddToCartQuery query)
		{
			await mediator.Send(query);
			return Ok();
		}

		/// <summary>
		/// Retrieves the shopping cart for the specified user.
		/// </summary>
		/// <param name="userId">The unique identifier of the user.</param>
		/// <returns>An action result containing the shopping cart details.</returns>
		[HttpGet]
		public async Task<IActionResult> GetCart([FromQuery] Guid userId)
		{
			var result = await mediator.Send(new GetShoppingCartQuery { UserId = userId });
			return Ok(result);
		}

		/// <summary>
		/// Removes an item from the user's shopping cart.
		/// </summary>
		/// <param name="query">The query containing the item details.</param>
		/// <returns>An action result indicating the outcome.</returns>
		[HttpPost("remove")]
		public async Task<IActionResult> RemoveFromCart(RemoveFromCartQuery query)
		{
			await mediator.Send(query);
			return Ok();
		}
	}
}
