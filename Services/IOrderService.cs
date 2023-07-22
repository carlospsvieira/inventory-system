using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_system.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrders();
        Task<Order> GetOrderById(int id);
        Task<List<Order>> CreateNewOrder(Order newOrder);
    }
}