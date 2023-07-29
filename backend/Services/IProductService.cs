using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_system.Services
{
  public interface IProductService
  {
    Task<ServiceResponse<List<Product>>> GetAllProducts();
    Task<ServiceResponse<Product>> GetProductById(int id);
    Task<ServiceResponse<Product>> UpdateProduct(Product updatedProduct);
    Task<ServiceResponse<List<Product>>> DeleteProduct(int id);
  }
}