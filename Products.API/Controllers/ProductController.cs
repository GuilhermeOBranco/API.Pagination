using Microsoft.AspNetCore.Mvc;
using Products.Domain.Filters;
using Products.Domain.Models;
using Products.Service;

namespace Products.API.Controllers;

[ApiController, Route("api/[controller]")]
public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost, Route("InsertProduct")]
    public async Task<IActionResult> InsertProduct([FromBody] Product product)
    {
        return Ok(await _productService.Insert(product));
    }

    [HttpPost, Route("GetProduct")]
    public async Task<IActionResult> GetProduct([FromQuery] FilteredParameters parameters, [FromBody] List<OrderByParameter> orderParams)
    {
        if (parameters.Page == 0)
            return BadRequest("The page is required");
        
        return Ok(await _productService.Get(parameters, orderParams));
    }
}