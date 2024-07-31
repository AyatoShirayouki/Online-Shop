using FluentValidation;
using MediatR;
using OnlineShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.ShoppingCarts.Queries.RemoveFromCart
{
	/// <summary>
	/// Handles the <see cref="RemoveFromCartQuery"/> and manages the removal of items from the shopping cart.
	/// </summary>
	public class RemoveFromCartQueryHandler : IRequestHandler<RemoveFromCartQuery>
	{
		private readonly IShoppingCartRepository shoppingCartRepository;
		private readonly IValidator<RemoveFromCartQuery> validator;

		/// <summary>
		/// Initializes a new instance of the <see cref="RemoveFromCartQueryHandler"/> class.
		/// </summary>
		/// <param name="shoppingCartRepository">The repository for shopping cart operations.</param>
		/// <param name="validator">The validator for validating the query.</param>
		public RemoveFromCartQueryHandler(IShoppingCartRepository shoppingCartRepository, IValidator<RemoveFromCartQuery> validator)
		{
			this.shoppingCartRepository = shoppingCartRepository;
			this.validator = validator;
		}

		/// <summary>
		/// Handles the query to remove an item from the shopping cart.
		/// </summary>
		/// <param name="request">The request object containing the query parameters.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		public async Task<Unit> Handle(RemoveFromCartQuery request, CancellationToken cancellationToken)
		{
			await validator.ValidateAndThrowAsync(request, cancellationToken);

			var cart = await shoppingCartRepository.GetByUserId(request.UserId);

			if (cart != null)
			{
				var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == request.ProductId);
				if (cartItem != null)
				{
					cart.Items.Remove(cartItem);
					await shoppingCartRepository.Update(cart);
				}
			}

			return Unit.Value;
		}
	}
}
