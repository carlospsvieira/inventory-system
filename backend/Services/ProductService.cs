using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_system.Services
{
  public class ProductService : IProductService
  {
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public ProductService(IMapper mapper, DataContext context)
    {
      _context = context;
      _mapper = mapper;

    }
    public async Task<ServiceResponse<List<Product>>> DeleteProduct(int id)
    {
      var serviceResponse = new ServiceResponse<List<Product>>();

      try
      {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
          throw new Exception($"Product with Id '{id}' was not found.");

        _context.Products.Remove(product);

        await _context.SaveChangesAsync();

        var products = await _context.Products.ToListAsync();

        serviceResponse.Data = products;
        serviceResponse.Message = $"Product with Id '{id}' was deleted.";
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }

      return serviceResponse;
    }

    public async Task<ServiceResponse<List<Product>>> GetAllProducts()
    {
      var serviceResponse = new ServiceResponse<List<Product>>();

      try
      {
        var products = await _context.Products.ToListAsync();
        serviceResponse.Data = products;
        serviceResponse.Message = "OK";
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }

      return serviceResponse;
    }

    public async Task<ServiceResponse<Product>> GetProductById(int id)
    {
      var serviceResponse = new ServiceResponse<Product>();

      try
      {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
          throw new Exception($"Product with Id '{id}' was not found.");

        serviceResponse.Data = product;
        serviceResponse.Message = "OK";
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }

      return serviceResponse;
    }

    public async Task<ServiceResponse<Product>> UpdateProduct(Product updatedProduct)
    {
      var serviceResponse = new ServiceResponse<Product>();

      try
      {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == updatedProduct.Id);

        if (product == null)
          throw new Exception($"Product with id '{updatedProduct.Id}' was not found.");

        _mapper.Map(updatedProduct, product);

        product.EntryDate = DateTime.Now;

        await _context.SaveChangesAsync();

        serviceResponse.Data = product;
        serviceResponse.Message = $"Product with Id '{updatedProduct.Id}' was updated.";
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }

      return serviceResponse;
    }
  }
}