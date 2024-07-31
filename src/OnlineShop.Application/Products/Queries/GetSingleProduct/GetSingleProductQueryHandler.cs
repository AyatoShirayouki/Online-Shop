using AutoMapper;
using FluentValidation;
using MediatR;
using OnlineShop.Application.Common.Interfaces;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Application.Products.Queries.GetSingleProduct;

/// <summary>
/// Handles the <see cref="GetSingleProductQuery"/> and returns a product model.
/// </summary>
public class GetSingleProductQueryHandler : IRequestHandler<GetSingleProductQuery, ProductModel>
{
    private readonly IReadRepository<Product> productRepository;
    private readonly IMapper mapper;
	private readonly IValidator<GetSingleProductQuery> validator;

	/// <summary>
	/// Initializes a new instance of the <see cref="GetSingleProductQueryHandler"/> class.
	/// </summary>
	/// <param name="productRepository">The repository to retrieve products from.</param>
	/// <param name="mapper">The AutoMapper instance for mapping entities to models.</param>
	/// <param name="validator">The validator for validating the query.</param>
	public GetSingleProductQueryHandler(IReadRepository<Product> productRepository, IMapper mapper, IValidator<GetSingleProductQuery> validator)
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
		this.validator = validator;
	}

	/// <summary>
	/// Handles the query and returns a product model.
	/// </summary>
	/// <param name="request">The request object containing the query parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The product model.</returns>
	public async Task<ProductModel> Handle(GetSingleProductQuery request, CancellationToken cancellationToken)
    {
		await validator.ValidateAndThrowAsync(request, cancellationToken);

		Product productEntity = await this.productRepository.Get(request.Id);
        return mapper.Map<ProductModel>(productEntity);
    }
}
