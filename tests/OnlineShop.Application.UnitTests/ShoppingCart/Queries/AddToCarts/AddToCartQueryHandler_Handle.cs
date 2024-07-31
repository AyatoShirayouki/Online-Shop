using FluentValidation;
using Moq;
using OnlineShop.Application.Common.Interfaces;
using OnlineShop.Application.ShoppingCarts.Queries.AddToCart;
using OnlineShop.Application.Validations;
using OnlineShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnlineShop.Application.UnitTests.ShoppingCarts.Queries.AddToCart
{
	/// <summary>
	/// Unit tests for <see cref="AddToCartQueryHandler"/> class.
	/// </summary>
	public class AddToCartQueryHandler_Handle
	{
		private readonly Mock<IShoppingCartRepository> shoppingCartRepositoryMock;
		private readonly Mock<IReadRepository<Product>> productRepositoryMock;
		private readonly IValidator<AddToCartQuery> validator;

		public AddToCartQueryHandler_Handle()
		{
			shoppingCartRepositoryMock = new Mock<IShoppingCartRepository>();
			productRepositoryMock = new Mock<IReadRepository<Product>>();
			validator = new AddToCartQueryValidator();
		}

		/// <summary>
		/// Test case to verify that a valid request adds the product to the cart.
		/// It checks if the product is successfully added and the cart is updated.
		/// </summary>
		[Fact]
		public async Task Handle_ValidRequest_AddsProductToCart()
		{
			// Arrange
			var userId = Guid.NewGuid();
			var productId = Guid.NewGuid();
			var product = new Product { Id = productId };
			var cart = new ShoppingCart { UserId = userId };
			shoppingCartRepositoryMock.Setup(repo => repo.GetByUserId(userId)).ReturnsAsync(cart);
			productRepositoryMock.Setup(repo => repo.Get(productId)).ReturnsAsync(product);

			var handler = new AddToCartQueryHandler(shoppingCartRepositoryMock.Object, productRepositoryMock.Object, validator);
			var query = new AddToCartQuery { UserId = userId, ProductId = productId, Quantity = 1 };
			var cancellationToken = new CancellationToken();

			// Act
			await handler.Handle(query, cancellationToken);

			// Assert
			Assert.Contains(cart.Items, item => item.ProductId == productId && item.Quantity == 1);
			shoppingCartRepositoryMock.Verify(repo => repo.Add(It.IsAny<ShoppingCart>()), Times.Never);
			shoppingCartRepositoryMock.Verify(repo => repo.Update(cart), Times.Once);
		}

		/// <summary>
		/// Test case to verify that an invalid request (e.g., missing user ID or product ID, or zero quantity) throws a validation exception.
		/// </summary>
		[Fact]
		public async Task Handle_InvalidRequest_ThrowsValidationException()
		{
			// Arrange
			var handler = new AddToCartQueryHandler(shoppingCartRepositoryMock.Object, productRepositoryMock.Object, validator);
			var query = new AddToCartQuery { UserId = Guid.Empty, ProductId = Guid.Empty, Quantity = 0 };
			var cancellationToken = new CancellationToken();

			// Act & Assert
			await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(query, cancellationToken));
		}

		/// <summary>
		/// Test case to verify that if the product already exists in the cart, the quantity is increased.
		/// </summary>
		[Fact]
		public async Task Handle_ExistingCartItem_IncreasesQuantity()
		{
			// Arrange
			var userId = Guid.NewGuid();
			var productId = Guid.NewGuid();
			var product = new Product { Id = productId };
			var cartItem = new CartItem { ProductId = productId, Quantity = 1 };
			var cart = new ShoppingCart { UserId = userId, Items = new List<CartItem> { cartItem } };
			shoppingCartRepositoryMock.Setup(repo => repo.GetByUserId(userId)).ReturnsAsync(cart);
			productRepositoryMock.Setup(repo => repo.Get(productId)).ReturnsAsync(product);

			var handler = new AddToCartQueryHandler(shoppingCartRepositoryMock.Object, productRepositoryMock.Object, validator);
			var query = new AddToCartQuery { UserId = userId, ProductId = productId, Quantity = 1 };
			var cancellationToken = new CancellationToken();

			// Act
			await handler.Handle(query, cancellationToken);

			// Assert
			Assert.Equal(2, cartItem.Quantity);
			shoppingCartRepositoryMock.Verify(repo => repo.Update(cart), Times.Once);
		}

		/// <summary>
		/// Test case to verify that if the product quantity is decreased in the cart, the quantity is updated accordingly.
		/// </summary>
		[Fact]
		public async Task Handle_ExistingCartItem_DecreasesQuantity()
		{
			// Arrange
			var userId = Guid.NewGuid();
			var productId = Guid.NewGuid();
			var product = new Product { Id = productId };
			var cartItem = new CartItem { ProductId = productId, Quantity = 2 };
			var cart = new ShoppingCart { UserId = userId, Items = new List<CartItem> { cartItem } };
			shoppingCartRepositoryMock.Setup(repo => repo.GetByUserId(userId)).ReturnsAsync(cart);
			productRepositoryMock.Setup(repo => repo.Get(productId)).ReturnsAsync(product);

			var handler = new AddToCartQueryHandler(shoppingCartRepositoryMock.Object, productRepositoryMock.Object, validator);
			var query = new AddToCartQuery { UserId = userId, ProductId = productId, Quantity = -1 };
			var cancellationToken = new CancellationToken();

			// Act
			await handler.Handle(query, cancellationToken);

			// Assert
			Assert.Equal(1, cartItem.Quantity);
			shoppingCartRepositoryMock.Verify(repo => repo.Update(cart), Times.Once);
		}

		/// <summary>
		/// Test case to verify that if the quantity of a product in the cart is reduced to zero or less, the product is removed from the cart.
		/// </summary>
		[Fact]
		public async Task Handle_ExistingCartItem_RemovesItemIfQuantityIsZeroOrLess()
		{
			// Arrange
			var userId = Guid.NewGuid();
			var productId = Guid.NewGuid();
			var product = new Product { Id = productId };
			var cartItem = new CartItem { ProductId = productId, Quantity = 1 };
			var cart = new ShoppingCart { UserId = userId, Items = new List<CartItem> { cartItem } };
			shoppingCartRepositoryMock.Setup(repo => repo.GetByUserId(userId)).ReturnsAsync(cart);
			productRepositoryMock.Setup(repo => repo.Get(productId)).ReturnsAsync(product);

			var handler = new AddToCartQueryHandler(shoppingCartRepositoryMock.Object, productRepositoryMock.Object, validator);
			var query = new AddToCartQuery { UserId = userId, ProductId = productId, Quantity = -1 };
			var cancellationToken = new CancellationToken();

			// Act
			await handler.Handle(query, cancellationToken);

			// Assert
			Assert.DoesNotContain(cart.Items, item => item.ProductId == productId);
			shoppingCartRepositoryMock.Verify(repo => repo.Update(cart), Times.Once);
		}

		/// <summary>
		/// Test case to verify that when adding a product to a new cart, the Add method is called.
		/// </summary>
		[Fact]
		public async Task Handle_NewCart_CallsAdd()
		{
			// Arrange
			var userId = Guid.NewGuid();
			var productId = Guid.NewGuid();
			var product = new Product { Id = productId };
			ShoppingCart? cart = null;
			shoppingCartRepositoryMock.Setup(repo => repo.GetByUserId(userId)).ReturnsAsync(cart);
			productRepositoryMock.Setup(repo => repo.Get(productId)).ReturnsAsync(product);

			var handler = new AddToCartQueryHandler(shoppingCartRepositoryMock.Object, productRepositoryMock.Object, validator);
			var query = new AddToCartQuery { UserId = userId, ProductId = productId, Quantity = 1 };
			var cancellationToken = new CancellationToken();

			// Act
			await handler.Handle(query, cancellationToken);

			// Assert
			shoppingCartRepositoryMock.Verify(repo => repo.Add(It.Is<ShoppingCart>(c => c.UserId == userId && c.Items.Any(i => i.ProductId == productId))), Times.Once);
			shoppingCartRepositoryMock.Verify(repo => repo.Update(It.IsAny<ShoppingCart>()), Times.Never);
		}

		/// <summary>
		/// Test case to verify that decreasing the quantity of a product in a non-existing cart throws a validation exception.
		/// </summary>
		[Fact]
		public async Task Handle_DecreaseQuantityForNonExistingCart_ThrowsValidationException()
		{
			// Arrange
			var userId = Guid.NewGuid();
			var productId = Guid.NewGuid();
			var product = new Product { Id = productId };
			ShoppingCart? cart = null;
			shoppingCartRepositoryMock.Setup(repo => repo.GetByUserId(userId)).ReturnsAsync(cart);
			productRepositoryMock.Setup(repo => repo.Get(productId)).ReturnsAsync(product);

			var handler = new AddToCartQueryHandler(shoppingCartRepositoryMock.Object, productRepositoryMock.Object, validator);
			var query = new AddToCartQuery { UserId = userId, ProductId = productId, Quantity = -1 };
			var cancellationToken = new CancellationToken();

			// Act & Assert
			await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(query, cancellationToken));
		}

		/// <summary>
		/// Test case to verify that decreasing the quantity of a non-existing product in the cart throws a validation exception.
		/// </summary>
		[Fact]
		public async Task Handle_DecreaseQuantityForNonExistingProductInCart_ThrowsValidationException()
		{
			// Arrange
			var userId = Guid.NewGuid();
			var productId = Guid.NewGuid();
			var product = new Product { Id = productId };
			var cart = new ShoppingCart { UserId = userId, Items = new List<CartItem>() };
			shoppingCartRepositoryMock.Setup(repo => repo.GetByUserId(userId)).ReturnsAsync(cart);
			productRepositoryMock.Setup(repo => repo.Get(productId)).ReturnsAsync(product);

			var handler = new AddToCartQueryHandler(shoppingCartRepositoryMock.Object, productRepositoryMock.Object, validator);
			var query = new AddToCartQuery { UserId = userId, ProductId = productId, Quantity = -1 };
			var cancellationToken = new CancellationToken();

			// Act & Assert
			await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(query, cancellationToken));
		}

		/// <summary>
		/// Test case to verify that trying to add an invalid product to the cart throws a validation exception.
		/// </summary>
		[Fact]
		public async Task Handle_InvalidProduct_ThrowsValidationException()
		{
			// Arrange
			var userId = Guid.NewGuid();
			var productId = Guid.NewGuid();
			Product? product = null;
			ShoppingCart? cart = null;
			shoppingCartRepositoryMock.Setup(repo => repo.GetByUserId(userId)).ReturnsAsync(cart);
			productRepositoryMock.Setup(repo => repo.Get(productId)).ReturnsAsync(product);

			var handler = new AddToCartQueryHandler(shoppingCartRepositoryMock.Object, productRepositoryMock.Object, validator);
			var query = new AddToCartQuery { UserId = userId, ProductId = productId, Quantity = 1 };
			var cancellationToken = new CancellationToken();

			// Act & Assert
			await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(query, cancellationToken));
		}
	}
}
