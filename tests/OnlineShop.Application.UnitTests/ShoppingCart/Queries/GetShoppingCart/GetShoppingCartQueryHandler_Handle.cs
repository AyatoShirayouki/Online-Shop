using AutoMapper;
using FluentValidation;
using Moq;
using OnlineShop.Application.Common.Interfaces;
using OnlineShop.Application.Common.Mappings;
using OnlineShop.Application.Services;
using OnlineShop.Application.ShoppingCarts.Queries.GetShoppingCart;
using OnlineShop.Application.Validations;
using OnlineShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnlineShop.Application.UnitTests.ShoppingCarts.Queries.GetShoppingCart
{
	/// <summary>
	/// Unit tests for <see cref="GetShoppingCartQueryHandler"/> class.
	/// </summary>
	public class GetShoppingCartQueryHandler_Handle
	{
		private readonly Mock<IShoppingCartRepository> shoppingCartRepositoryMock;
		private readonly Mock<IReadRepository<Product>> productRepositoryMock;
		private readonly IMapper mapper;
		private readonly IValidator<GetShoppingCartQuery> validator;
		private readonly Mock<DiscountCalculationService> discountServiceMock;

		public GetShoppingCartQueryHandler_Handle()
		{
			shoppingCartRepositoryMock = new Mock<IShoppingCartRepository>();
			productRepositoryMock = new Mock<IReadRepository<Product>>();
			mapper = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())).CreateMapper();
			validator = new GetShoppingCartQueryValidator();
			discountServiceMock = new Mock<DiscountCalculationService>(MockBehavior.Loose, productRepositoryMock.Object);
		}

		/// <summary>
		/// Test case to verify that a valid request returns the expected shopping cart model.
		/// </summary>
		[Fact]
		public async Task Handle_ValidRequest_ReturnsShoppingCartModel()
		{
			// Arrange
			var userId = Guid.NewGuid();
			var cart = new ShoppingCart { UserId = userId };
			shoppingCartRepositoryMock.Setup(repo => repo.GetByUserId(userId)).ReturnsAsync(cart);

			var handler = new GetShoppingCartQueryHandler(shoppingCartRepositoryMock.Object, productRepositoryMock.Object, mapper, discountServiceMock.Object, validator);
			var query = new GetShoppingCartQuery { UserId = userId };
			var cancellationToken = new CancellationToken();

			// Act
			var result = await handler.Handle(query, cancellationToken);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(cart.UserId, userId);
		}

		/// <summary>
		/// Test case to verify that an invalid request (e.g., empty user ID) throws a validation exception.
		/// </summary>
		[Fact]
		public async Task Handle_InvalidRequest_ThrowsValidationException()
		{
			// Arrange
			var handler = new GetShoppingCartQueryHandler(shoppingCartRepositoryMock.Object, productRepositoryMock.Object, mapper, discountServiceMock.Object, validator);
			var query = new GetShoppingCartQuery { UserId = Guid.Empty };
			var cancellationToken = new CancellationToken();

			// Act & Assert
			await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(query, cancellationToken));
		}

		/// <summary>
		/// Test case to verify that when the cart has no products, the handler returns an empty shopping cart model.
		/// </summary>
		[Fact]
		public async Task Handle_NoProductsInCart_ReturnsEmptyShoppingCartModel()
		{
			// Arrange
			var userId = Guid.NewGuid();
			var cart = new ShoppingCart { UserId = userId };
			shoppingCartRepositoryMock.Setup(repo => repo.GetByUserId(userId)).ReturnsAsync(cart);

			var handler = new GetShoppingCartQueryHandler(shoppingCartRepositoryMock.Object, productRepositoryMock.Object, mapper, discountServiceMock.Object, validator);
			var query = new GetShoppingCartQuery { UserId = userId };
			var cancellationToken = new CancellationToken();

			// Act
			var result = await handler.Handle(query, cancellationToken);

			// Assert
			Assert.NotNull(result);
			Assert.Empty(result.Items);
		}
	}
}
