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

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<Product>>> GetById(int id)
    {
      var product = await _productService.GetProductById(id);

      if (product.Success == false)
      {
        return NotFound(product);
      }

      return Ok(product);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<List<Product>>>> DeleteProduct(int id)
    {
      var products = await _productService.DeleteProduct(id);

      if (products.Success == false)
      {
        return NotFound(products);
      }

      return Ok(products);
    }

    [HttpPut("{id}/edit")]
    public async Task<ActionResult<ServiceResponse<Product>>> UpdateProduct(Order updatedProduct)
    {
      var products = await _productService.UpdateProduct(updatedProduct);

      if (products.Success == false)
      {
        return NotFound(products);
      }

      return Ok(products);
    }

  }
}