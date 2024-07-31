using FluentValidation;
using OnlineShop.Application.Products.Queries.GetSingleProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Validations
{
	/// <summary>
	/// Validator for the <see cref="GetSingleProductQuery"/>.
	/// </summary>
	public class GetSingleProductQueryValidator : AbstractValidator<GetSingleProductQuery>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GetSingleProductQueryValidator"/> class.
		/// </summary>
		public GetSingleProductQueryValidator()
		{
			RuleFor(x => x.Id).NotEmpty().WithMessage("Product ID is required.");
		}
	}
}
