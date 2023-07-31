using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_system.Services
{
  public interface IOrderService
  {
    Task<ServiceResponse<List<Order>>> GetAllOrders();
    Task<ServiceResponse<Order>> GetOrderById(int id);
    Task<ServiceResponse<Order>> CreateNewOrder(Order newOrder);
    Task<ServiceResponse<OrderItem>> AddOrderItem(int id, OrderItem newOrderItem);
    Task<ServiceResponse<List<Order>>> DeleteOrder(int id);
    Task<ServiceResponse<Order>> CompleteOrder(Order completeOrder);
  }
}