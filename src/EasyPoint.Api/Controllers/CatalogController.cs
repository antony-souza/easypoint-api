using EasyPoint.Application.Modules.Catalog.Products;
using Microsoft.AspNetCore.Mvc;

namespace EasyPoint.Api.Controllers;

[ApiController]
[Route("/catalog")]
public class CatalogController : ControllerBase
{
    private readonly ListAllProductsUseCase _listAllProductsUseCase;

    public CatalogController(ListAllProductsUseCase listAllProductsUseCase)
    {
        _listAllProductsUseCase = listAllProductsUseCase;
    }

    [HttpGet]
    [Route("products/{id}")]
    public IActionResult GetProducts([FromRoute] Guid id)
    {
        var products = _listAllProductsUseCase.Handler(id);
        return Ok(products);
    }
    
}