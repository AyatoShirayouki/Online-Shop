using AutoMapper;
using FluentValidation;
using Moq;
using OnlineShop.Application.Common.Interfaces;
using OnlineShop.Application.Common.Mappings;
using OnlineShop.Application.Products.Queries.GetSingleProduct;
using OnlineShop.Application.Validations;
using OnlineShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnlineShop.Application.UnitTests.Products.Queries.GetSingleProduct
{
	/// <summary>
	/// Unit tests for <see cref="GetSingleProductQueryHandler"/> class.
	/// </summary>
	public class GetSingleProductQueryHandler_Handle
	{
		private readonly Mock<IReadRepository<Product>> productRepositoryMock;
		private readonly IMapper mapper;
		private readonly IValidator<GetSingleProductQuery> validator;

		public GetSingleProductQueryHandler_Handle()
		{
			productRepositoryMock = new Mock<IReadRepository<Product>>();
			mapper = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())).CreateMapper();
			validator = new GetSingleProductQueryValidator();
		}

		/// <summary>
		/// Test case to verify that a valid request returns the expected product model.
		/// </summary>
		[Fact]
		public async Task Handle_ValidRequest_ReturnsProductModel()
		{
			// Arrange
			var productId = Guid.NewGuid();
			var product = new Product { Id = productId, Name = "Test Product" };
			productRepositoryMock.Setup(repo => repo.Get(productId)).ReturnsAsync(product);

			var handler = new GetSingleProductQueryHandler(productRepositoryMock.Object, mapper, validator);
			var query = new GetSingleProductQuery { Id = productId };
			var cancellationToken = new CancellationToken();

			// Act
			var result = await handler.Handle(query, cancellationToken);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(productId, result.Id);
			Assert.Equal("Test Product", result.Name);
		}

		/// <summary>
		/// Test case to verify that an invalid request (e.g., empty product ID) throws a validation exception.
		/// </summary>
		[Fact]
		public async Task Handle_InvalidRequest_ThrowsValidationException()
		{
			// Arrange
			var handler = new GetSingleProductQueryHandler(productRepositoryMock.Object, mapper, validator);
			var query = new GetSingleProductQuery { Id = Guid.Empty };
			var cancellationToken = new CancellationToken();

			// Act & Assert
			await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(query, cancellationToken));
		}

		/// <summary>
		/// Test case to verify that when the product is not found in the repository, 
		/// the handler returns null.
		/// </summary>
		[Fact]
		public async Task Handle_ProductNotFound_ReturnsNull()
		{
			// Arrange
			var productId = Guid.NewGuid();
			productRepositoryMock.Setup(repo => repo.Get(productId)).ReturnsAsync((Product)null);

			var handler = new GetSingleProductQueryHandler(productRepositoryMock.Object, mapper, validator);
			var query = new GetSingleProductQuery { Id = productId };
			var cancellationToken = new CancellationToken();

			// Act
			var result = await handler.Handle(query, cancellationToken);

			// Assert
			Assert.Null(result);
		}
	}
}
