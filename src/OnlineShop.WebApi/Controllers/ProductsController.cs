using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.WebApi.Controllers;

/// <summary>
/// API Controller for managing product-related operations.
/// </summary>
[ApiController]
[Route("[controller]")]
public partial class ProductsController : ControllerBase
{
    private readonly IMediator mediator;

	/// <summary>
	/// Initializes a new instance of the <see cref="ProductsController"/> class.
	/// </summary>
	/// <param name="mediator">The mediator for handling requests.</param>
	public ProductsController(IMediator mediator)
    {
        this.mediator = mediator;
    }
}
