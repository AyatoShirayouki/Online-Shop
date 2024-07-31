using FluentValidation;
using OnlineShop.Application.ShoppingCarts.Queries.RemoveFromCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Validations
{
	/// <summary>
	/// Validator for the <see cref="RemoveFromCartQuery"/>.
	/// </summary>
	public class RemoveFromCartQueryValidator : AbstractValidator<RemoveFromCartQuery>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RemoveFromCartQueryValidator"/> class.
		/// </summary>
		public RemoveFromCartQueryValidator()
		{
			RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required.");
			RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product ID is required.");
		}
	}
}
