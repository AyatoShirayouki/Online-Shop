using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Products.Queries.GetSingleProduct;

namespace OnlineShop.WebApi.Controllers;

public partial class ProductsController
{
    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<ProductModel>> GetSingleProduct(Guid id)
    {
        GetSingleProductQuery query = new() { Id = id };
        ProductModel productModel = await this.mediator.Send(query);
        return Ok(productModel);
    }
}

