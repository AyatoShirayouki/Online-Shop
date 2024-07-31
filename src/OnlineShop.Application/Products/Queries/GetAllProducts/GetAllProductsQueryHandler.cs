using AutoMapper;
using MediatR;
using OnlineShop.Application.Common.Interfaces;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Application.Products.Queries.GetAllProducts;

/// <summary>
/// Handles the <see cref="GetAllProductsQuery"/> and returns a list of product models.
/// </summary>
public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductModel>>
{
    private readonly IReadRepository<Product> productRepository;
    private readonly IMapper mapper;

	/// <summary>
	/// Initializes a new instance of the <see cref="GetAllProductsQueryHandler"/> class.
	/// </summary>
	/// <param name="productRepository">The repository to retrieve products from.</param>
	/// <param name="mapper">The AutoMapper instance for mapping entities to models.</param>
	public GetAllProductsQueryHandler(IReadRepository<Product> productRepository, IMapper mapper)
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
    }

	/// <summary>
	/// Handles the query and returns a list of product models.
	/// </summary>
	/// <param name="request">The request object containing the query parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of product models.</returns>
	public async Task<IEnumerable<ProductModel>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        ISpecification<Product> specification = new ProductsSortedAscByNameSpec();
        IEnumerable<Product> productEntities = await this.productRepository.Get(specification);
        return mapper.Map<IEnumerable<ProductModel>>(productEntities);
    }
}
