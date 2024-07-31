using FluentValidation;
using MediatR;
using OnlineShop.Application.Common.Interfaces;
using OnlineShop.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Application.ShoppingCarts.Queries.AddToCart
{
	/// <summary>
	/// Handles the <see cref="AddToCartQuery"/> and manages the addition of items to the shopping cart.
	/// </summary>
	public class AddToCartQueryHandler : IRequestHandler<AddToCartQuery>
	{
		private readonly IShoppingCartRepository shoppingCartRepository;
		private readonly IReadRepository<Product> productRepository;
		private readonly IValidator<AddToCartQuery> validator;

		/// <summary>
		/// Initializes a new instance of the <see cref="AddToCartQueryHandler"/> class.
		/// </summary>
		/// <param name="shoppingCartRepository">The repository for shopping cart operations.</param>
		/// <param name="productRepository">The repository to retrieve products from.</param>
		/// <param name="validator">The validator for validating the query.</param>
		public AddToCartQueryHandler(IShoppingCartRepository shoppingCartRepository, IReadRepository<Product> productRepository, IValidator<AddToCartQuery> validator)
		{
			this.shoppingCartRepository = shoppingCartRepository;
			this.productRepository = productRepository;
			this.validator = validator;
		}

		/// <summary>
		/// Handles the query to add an item to the shopping cart.
		/// </summary>
		/// <param name="request">The request object containing the query parameters.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		public async Task<Unit> Handle(AddToCartQuery request, CancellationToken cancellationToken)
		{
			await validator.ValidateAndThrowAsync(request, cancellationToken);

			var product = await productRepository.Get(request.ProductId);
			if (product == null)
			{
				throw new ValidationException("The product does not exist.");
			}

			var cart = await shoppingCartRepository.GetByUserId(request.UserId);

			if (cart == null)
			{
				if (request.Quantity < 0)
				{
					throw new ValidationException("Cannot decrease quantity for a non-existing cart.");
				}

				cart = new ShoppingCart { UserId = request.UserId };
				cart.Items.Add(new CartItem { ProductId = request.ProductId, Quantity = request.Quantity });
				await shoppingCartRepository.Add(cart);
			}
			else
			{
				var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == request.ProductId);
				if (cartItem != null)
				{
					cartItem.Quantity += request.Quantity;

					if (cartItem.Quantity <= 0)
					{
						cart.Items.Remove(cartItem);
					}
				}
				else
				{
					if (request.Quantity < 0)
					{
						throw new ValidationException("Cannot decrease quantity for a non-existing product in the cart.");
					}

					cart.Items.Add(new CartItem { ProductId = request.ProductId, Quantity = request.Quantity });
				}
				await shoppingCartRepository.Update(cart);
			}

			return Unit.Value;
		}
	}
}
