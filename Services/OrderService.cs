using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_system.Services
{
  public class OrderService : IOrderService
  {
    private static List<Order> orders = new List<Order> {
      new Order {Id = 1, Name = "Cookie", Category = Categories.PantryStaples, Supplier = "Nestle", Quantity = 10000},
      new Order {Id = 2, Name = "Ham", Category = Categories.DairyAndEgg, Supplier = "Dairy Deals", Weight = 0.5, Quantity = 100}
    };

    public async Task<ServiceResponse<List<Order>>> CreateNewOrder(Order newOrder)
    {
      var serviceResponse = new ServiceResponse<List<Order>>();

      try
      {
        newOrder.EntryDate = DateTime.Now;
        newOrder.Id = orders.Max(o => o.Id) + 1;
        orders.Add(newOrder);
        serviceResponse.Data = orders;
        serviceResponse.Message = "OK";
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<Order>>> GetAllOrders()
    {
      var serviceResponse = new ServiceResponse<List<Order>>();

      try
      {
        serviceResponse.Data = orders;
        serviceResponse.Message = "OK";

      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }
      return serviceResponse;
    }

    public async Task<ServiceResponse<Order>> GetOrderById(int id)
    {
      var serviceResponse = new ServiceResponse<Order>();

      try
      {
        var order = orders.FirstOrDefault(o => o.Id == id);

        if (order == null)
          throw new Exception($"Order with Id '{id}' was not found.");

        serviceResponse.Data = order;
        serviceResponse.Message = "OK";
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<Order>>> UpdateOrder(Order updatedOrder)
    {
      var serviceResponse = new ServiceResponse<List<Order>>();

      try
      {
        var order = orders.FirstOrDefault(o => o.Id == updatedOrder.Id);

        if (order == null)
          throw new Exception($"Order with Id '{updatedOrder.Id}' was not found.");

        order.EntryDate = DateTime.Now;
        order.Name = updatedOrder.Name;
        order.Category = updatedOrder.Category;
        order.Supplier = updatedOrder.Supplier;
        order.Weight = updatedOrder.Weight;
        order.Quantity = updatedOrder.Quantity;

        serviceResponse.Data = orders;
        serviceResponse.Message = "OK";
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