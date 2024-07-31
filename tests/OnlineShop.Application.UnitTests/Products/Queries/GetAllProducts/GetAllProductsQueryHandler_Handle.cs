using AutoMapper;
using Moq;
using OnlineShop.Application.Common.Interfaces;
using OnlineShop.Application.Common.Mappings;
using OnlineShop.Application.Products.Queries.GetAllProducts;
using OnlineShop.Domain.Entities;
using Xunit;

namespace OnlineShop.Application.UnitTests.Products.Queries.GetAllProducts;

/// <summary>
/// Unit tests for <see cref="GetAllProductsQueryHandler"/> class.
/// </summary>
public class GetAllProductsQueryHandler_Handle
{

	/// <summary>
	/// Test case to verify that when there are no products in the repository, 
	/// the handler returns an empty list.
	/// </summary>
	[Fact]
	public async void When_NoProducts_Returns_EmptyList()
	{
        // Arange
        Mock<IReadRepository<Product>> productRepositoryMock = new();
        productRepositoryMock
            .Setup(x => x.Get(It.IsAny<ISpecification<Product>>()))
            .ReturnsAsync(Enumerable.Empty<Product>());

        IMapper mapper = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile())).CreateMapper();

        GetAllProductsQueryHandler handler = new (productRepositoryMock.Object, mapper);
        GetAllProductsQuery query = new();
        CancellationToken cancellationToken = new();

        // Act
        IEnumerable<ProductModel> models = await handler.Handle(query, cancellationToken);

        // Assert
        Assert.Empty(models);
    }

	/// <summary>
	/// Test case to verify that when there is one product in the repository, 
	/// the handler returns a list containing exactly one product model.
	/// </summary>
	[Fact]
    public async void When_OneProduct_Returns_ListWithOneItem()
    {
        // Arange
        Mock<IReadRepository<Product>> productRepositoryMock = new();
        productRepositoryMock
            .Setup(x => x.Get(It.IsAny<ISpecification<Product>>()))
            .ReturnsAsync(new List<Product> { new Product() });

        IMapper mapper = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile())).CreateMapper();

        GetAllProductsQueryHandler handler = new(productRepositoryMock.Object, mapper);
        GetAllProductsQuery query = new();
        CancellationToken cancellationToken = new();

        // Act
        IEnumerable<ProductModel> models = await handler.Handle(query, cancellationToken);

        // Assert
        Assert.Single(models);
    }

	/// <summary>
	/// Test case to verify that when there are multiple products in the repository, 
	/// the handler returns a list with the corresponding number of product models.
	/// </summary>
	[Fact]
    public async void When_ManyProduct_Returns_ManyItems()
    {
        // Arange
        Mock<IReadRepository<Product>> productRepositoryMock = new();
        productRepositoryMock
            .Setup(x => x.Get(It.IsAny<ISpecification<Product>>()))
            .ReturnsAsync(new List<Product> { new Product(), new Product(), new Product() });

        IMapper mapper = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile())).CreateMapper();

        GetAllProductsQueryHandler handler = new(productRepositoryMock.Object, mapper);
        GetAllProductsQuery query = new();
        CancellationToken cancellationToken = new();

        // Act
        IEnumerable<ProductModel> models = await handler.Handle(query, cancellationToken);

        // Assert
        Assert.Equal(3, models.Count());
    }
}

