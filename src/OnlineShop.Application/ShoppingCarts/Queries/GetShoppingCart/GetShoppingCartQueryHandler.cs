using AutoMapper;
using FluentValidation;
using MediatR;
using OnlineShop.Application.Common.Interfaces;
using OnlineShop.Application.Services;
using OnlineShop.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Application.ShoppingCarts.Queries.GetShoppingCart
{
	/// <summary>
	/// Handles the <see cref="GetShoppingCartQuery"/> and returns a shopping cart model.
	/// </summary>
	public class GetShoppingCartQueryHandler : IRequestHandler<GetShoppingCartQuery, ShoppingCartModel>
	{
		private readonly IShoppingCartRepository shoppingCartRepository;
		private readonly IReadRepository<Product> productRepository;
		private readonly IMapper mapper;
		private readonly DiscountCalculationService discountService;
		private readonly IValidator<GetShoppingCartQuery> validator;

		/// <summary>
		/// Initializes a new instance of the <see cref="GetShoppingCartQueryHandler"/> class.
		/// </summary>
		/// <param name="shoppingCartRepository">The repository for shopping cart operations.</param>
		/// <param name="productRepository">The repository to retrieve products from.</param>
		/// <param name="mapper">The AutoMapper instance for mapping entities to models.</param>
		/// <param name="discountService">The service for calculating discounts.</param>
		/// <param name="validator">The validator for validating the query.</param>
		public GetShoppingCartQueryHandler(IShoppingCartRepository shoppingCartRepository, 
			IReadRepository<Product> productRepository, IMapper mapper, 
			DiscountCalculationService discountService, IValidator<GetShoppingCartQuery> validator)
		{
			this.shoppingCartRepository = shoppingCartRepository;
			this.productRepository = productRepository;
			this.mapper = mapper;
			this.discountService = discountService;
			this.validator = validator;
		}

		/// <summary>
		/// Handles the query to retrieve a shopping cart model.
		/// </summary>
		/// <param name="request">The request object containing the query parameters.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The shopping cart model.</returns>
		public async Task<ShoppingCartModel> Handle(GetShoppingCartQuery request, CancellationToken cancellationToken)
		{
			await validator.ValidateAndThrowAsync(request, cancellationToken);

			var cart = await shoppingCartRepository.GetByUserId(request.UserId) ?? new ShoppingCart();
			var cartModel = new ShoppingCartModel();

			foreach (var item in cart.Items)
			{
				var product = await productRepository.Get(item.ProductId);
				if (product != null)
				{
					cartModel.Items.Add(new CartItemModel
					{
						ProductId = product.Id,
						ProductName = product.Name,
						Price = product.Price,
						Quantity = item.Quantity,
						TotalPrice = product.Price * item.Quantity
					});
				}
			}

			var (discount, discountDescription) = await discountService.CalculateDiscount(cart);
			cartModel.Discount = discount;
			cartModel.DiscountDescription = discountDescription;

			return mapper.Map<ShoppingCartModel>(cartModel);
		}
	}
}
