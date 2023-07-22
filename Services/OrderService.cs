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
      new Order {Name = "Ham", Category = Categories.DairyAndEgg, Supplier = "Nestle", Weight = 0.5, Quantity = 100}
    };

    public async Task<List<Order>> CreateNewOrder(Order newOrder)
    {
      newOrder.EntryDate = DateTime.Now;
      orders.Add(newOrder);
      return orders;
    }

    public async Task<List<Order>> GetAllOrders()
    {
      return orders;
    }

    public async Task<Order> GetOrderById(int id)
    {
      var order = orders.Find(o => o.Id == id);
      return order;
    }
  }
}