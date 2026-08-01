using EasyPoint.Application.Modules.Catalog.Products;
using Microsoft.AspNetCore.Mvc;

namespace EasyPoint.Api.Controllers.Catalogs.Products;

public class ProductController : ControllerBase
{
    private readonly ListAllProductsUseCase _listAllProductsUseCase;

    public ProductController(ListAllProductsUseCase listAllProductsUseCase)
    {
        _listAllProductsUseCase = listAllProductsUseCase;
    }

    [HttpGet("stores/{storeId}/products")]
    public async Task<IActionResult> GetAllProductsByStore(Guid storeId)
    {
        var products = await _listAllProductsUseCase.Handler(storeId);
        return Ok(products);
    }
}