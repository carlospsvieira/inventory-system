using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_system.Services
{
  public class ProductService : IProductService
  {

    private static List<Product> products = new List<Product> {
      new Product {Id = 1, Name = "Cookie", Category = Categories.PantryStaples, Supplier = "Nestle", Quantity = 10000},
      new Product {Id = 2, Name = "Ham", Category = Categories.DairyAndEgg, Supplier = "Dairy Deals", Weight = 0.5, Quantity = 100}
    };

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
        var product = products.FirstOrDefault(p => p.Id == id);

        if (product == null)
          throw new Exception($"Product with Id '{id}' was not found.");

        products.Remove(product);

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
      throw new NotImplementedException();
    }

    public async Task<ServiceResponse<Product>> UpdateProduct(Product updatedProduct)
    {
      throw new NotImplementedException();
    }
  }
}