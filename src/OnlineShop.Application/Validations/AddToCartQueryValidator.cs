using FluentValidation;
using OnlineShop.Application.ShoppingCarts.Queries.AddToCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Validations
{
	/// <summary>
	/// Validator for the <see cref="AddToCartQuery"/>.
	/// </summary>
	public class AddToCartQueryValidator : AbstractValidator<AddToCartQuery>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AddToCartQueryValidator"/> class.
		/// </summary>
		public AddToCartQueryValidator()
		{
			RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required.");
			RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product ID is required.");
			RuleFor(x => x.Quantity).NotEqual(0).WithMessage("Quantity must not be zero.");
		}
	}
}
