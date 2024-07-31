using OnlineShop.Application.Common.Interfaces;
using OnlineShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services
{
	/// <summary>
	/// Service responsible for calculating discounts on shopping carts.
	/// </summary>
	public class DiscountCalculationService
	{
		private readonly IReadRepository<Product> productRepository;

		/// <summary>
		/// Initializes a new instance of the <see cref="DiscountCalculationService"/> class.
		/// </summary>
		/// <param name="productRepository">The repository to retrieve products from.</param>
		public DiscountCalculationService(IReadRepository<Product> productRepository)
		{
			this.productRepository = productRepository;
		}

		/// <summary>
		/// Calculates the applicable discount for a shopping cart.
		/// </summary>
		/// <param name="cart">The shopping cart to calculate the discount for.</param>
		/// <returns>A tuple containing the discount amount and description.</returns>
		public async Task<(decimal discount, string description)> CalculateDiscount(ShoppingCart cart)
		{
			decimal discount = 0;
			string discountDescription = string.Empty;

			var productIds = cart.Items.Select(i => i.ProductId).ToArray();
			var products = await productRepository.Get(productIds);

			var totalPrice = cart.Items.Sum(i => i.Quantity * products.First(p => p.Id == i.ProductId).Price);
			var itemCount = cart.Items.Sum(i => i.Quantity);
			var cheapestItem = products.OrderBy(p => p.Price).FirstOrDefault();

			if (itemCount >= 5 && cheapestItem != null)
			{
				var freeItemDiscount = cheapestItem.Price;
				if (freeItemDiscount > discount)
				{
					discount = freeItemDiscount;
					discountDescription = $"Buy 5, get the cheapest item free ({cheapestItem.Name})";
				}
			}

			if (totalPrice > 1000)
			{
				var priceDiscount = 100;
				if (priceDiscount > discount)
				{
					discount = priceDiscount;
					discountDescription = "Spend over $1000, get $100 off";
				}
			}

			return (discount, discountDescription);
		}
	}
}
