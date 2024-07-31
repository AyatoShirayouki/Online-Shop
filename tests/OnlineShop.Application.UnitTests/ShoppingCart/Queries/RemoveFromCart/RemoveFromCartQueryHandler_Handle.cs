using FluentValidation;
using Moq;
using OnlineShop.Application.Common.Interfaces;
using OnlineShop.Application.ShoppingCarts.Queries.RemoveFromCart;
using OnlineShop.Application.Validations;
using OnlineShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnlineShop.Application.UnitTests.ShoppingCarts.Queries.RemoveFromCart
{
	/// <summary>
	/// Unit tests for <see cref="RemoveFromCartQueryHandler"/> class.
	/// </summary>
	public class RemoveFromCartQueryHandler_Handle
	{
		private readonly Mock<IShoppingCartRepository> shoppingCartRepositoryMock;
		private readonly IValidator<RemoveFromCartQuery> validator;

		public RemoveFromCartQueryHandler_Handle()
		{
			shoppingCartRepositoryMock = new Mock<IShoppingCartRepository>();
			validator = new RemoveFromCartQueryValidator();
		}

		/// <summary>
		/// Test case to verify that a valid request removes the product from the cart.
		/// </summary>
		[Fact]
		public async Task Handle_ValidRequest_RemovesProductFromCart()
		{
			// Arrange
			var userId = Guid.NewGuid();
			var productId = Guid.NewGuid();
			var cart = new ShoppingCart { UserId = userId };
			cart.Items.Add(new CartItem { ProductId = productId, Quantity = 1 });
			shoppingCartRepositoryMock.Setup(repo => repo.GetByUserId(userId)).ReturnsAsync(cart);

			var handler = new RemoveFromCartQueryHandler(shoppingCartRepositoryMock.Object, validator);
			var query = new RemoveFromCartQuery { UserId = userId, ProductId = productId };
			var cancellationToken = new CancellationToken();

			// Act
			await handler.Handle(query, cancellationToken);

			// Assert
			Assert.DoesNotContain(cart.Items, item => item.ProductId == productId);
			shoppingCartRepositoryMock.Verify(repo => repo.Update(cart), Times.Once);
		}

		/// <summary>
		/// Test case to verify that an invalid request (e.g., missing user ID or product ID) throws a validation exception.
		/// </summary>
		[Fact]
		public async Task Handle_InvalidRequest_ThrowsValidationException()
		{
			// Arrange
			var handler = new RemoveFromCartQueryHandler(shoppingCartRepositoryMock.Object, validator);
			var query = new RemoveFromCartQuery { UserId = Guid.Empty, ProductId = Guid.Empty };
			var cancellationToken = new CancellationToken();

			// Act & Assert
			await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(query, cancellationToken));
		}

		/// <summary>
		/// Test case to verify that if the product is not in the cart, no update is called.
		/// </summary>
		[Fact]
		public async Task Handle_ProductNotInCart_NoUpdateCalled()
		{
			// Arrange
			var userId = Guid.NewGuid();
			var productId = Guid.NewGuid();
			var cart = new ShoppingCart { UserId = userId };
			shoppingCartRepositoryMock.Setup(repo => repo.GetByUserId(userId)).ReturnsAsync(cart);

			var handler = new RemoveFromCartQueryHandler(shoppingCartRepositoryMock.Object, validator);
			var query = new RemoveFromCartQuery { UserId = userId, ProductId = productId };
			var cancellationToken = new CancellationToken();

			// Act
			await handler.Handle(query, cancellationToken);

			// Assert
			shoppingCartRepositoryMock.Verify(repo => repo.Update(It.IsAny<ShoppingCart>()), Times.Never);
		}
	}
}
