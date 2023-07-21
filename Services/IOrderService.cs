using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_system.Services
{
    public interface IOrderService
    {
        List<Order> GetAllOrders();
        Order GetOrderById(int id);
        List<Order> CreateNewOrder(Order newOrder);
    }
}