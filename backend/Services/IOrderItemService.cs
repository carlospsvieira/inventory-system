using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_system.Services
{
  public interface IOrderItemService
  {
    Task<ServiceResponse<OrderItem>> AddOrderItem(int orderId, OrderItem newOrderItem);
    Task<ServiceResponse<OrderItem>> UpdateOrderItem(OrderItem updatedItem);
  }
}