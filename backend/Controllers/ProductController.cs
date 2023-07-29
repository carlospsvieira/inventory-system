using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace inventory_system.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProductController : ControllerBase
  {
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
      _productService = productService;

    }

    [HttpGet]

    public async Task<ActionResult<ServiceResponse<List<Product>>>> Get()
    {
      var products = await _productService.GetAllProducts();

      if (products.Success == false)
      {
        return BadRequest(products);
      }

      return Ok(products);
    }
  }
}