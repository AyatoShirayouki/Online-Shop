using FluentValidation;
using OnlineShop.Application.ShoppingCarts.Queries.GetShoppingCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Validations
{
	/// <summary>
	/// Validator for the <see cref="GetShoppingCartQuery"/>.
	/// </summary>
	public class GetShoppingCartQueryValidator : AbstractValidator<GetShoppingCartQuery>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GetShoppingCartQueryValidator"/> class.
		/// </summary>
		public GetShoppingCartQueryValidator()
		{
			RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required.");
		}
	}
}
