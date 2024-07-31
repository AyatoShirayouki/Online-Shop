using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Products.Queries.GetAllProducts;

namespace OnlineShop.WebApi.Controllers;

public partial class ProductsController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductModel>>> GetAllProducts()
    {
        GetAllProductsQuery query = new();
        IEnumerable<ProductModel> productModels = await this.mediator.Send(query);
        return Ok(productModels);
    }
}
