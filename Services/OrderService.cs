using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_system.Services
{
  public class OrderService : IOrderService
  {
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public OrderService(IMapper mapper, DataContext context)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<ServiceResponse<Order>> CreateNewOrder(Order newOrder)
    {
      var serviceResponse = new ServiceResponse<Order>();

      try
      {
        newOrder.EntryDate = DateTime.Now;

        _context.Orders.Add(newOrder);

        await _context.SaveChangesAsync();

        serviceResponse.Data = newOrder;
        serviceResponse.Message = "Created";
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<Order>>> DeleteOrder(int id)
    {
      var serviceResponse = new ServiceResponse<List<Order>>();

      try
      {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
          throw new Exception("Not found.");

        _context.Orders.Remove(order);

        await _context.SaveChangesAsync();

        var orders = await _context.Orders.ToListAsync();

        serviceResponse.Data = orders;
        serviceResponse.Message = $"Order '{id}' has been deleted.";
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
        var orders = await _context.Orders.ToListAsync();
        serviceResponse.Data = orders;
        serviceResponse.Message = "OK. Reminder: Order Items will always be null at this endpoint for better performance.";
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
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
          throw new Exception("Not found.");

        var items = await _context.OrderItems.Where(item => item.OrderId == id).ToListAsync();
        order.Items = items;

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

    public async Task<ServiceResponse<Order>> CompleteOrder(Order completeOrder)
    {
      var serviceResponse = new ServiceResponse<Order>();

      try
      {
        if (completeOrder.Completed == false)
          throw new Exception("Request has failed. 'Completed' property is set as false.");

        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == completeOrder.Id);
        var items = await _context.OrderItems.Where(item => item.OrderId == completeOrder.Id).ToListAsync();

        if (order == null)
          throw new Exception($"Not found.");

        if (items != null)
        {
          foreach (OrderItem item in items)
          {
            item.EntryDate = DateTime.Now;

            var inventoryItem = _mapper.Map<InventoryItem>(item);
            _context.InventoryItems.Add(inventoryItem);
          }
        }
        else
        {
          serviceResponse.Message = "Completed. No items have been added to inventory.";
          _context.Orders.Remove(order);
          await _context.SaveChangesAsync();
          return serviceResponse;
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        serviceResponse.Message = "Completed. Items have been added to inventory.";
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }
      return serviceResponse;
    }

    public async Task<ServiceResponse<Order>> ChangeOrderTitle(int id, string newTitle)
    {
      var serviceResponse = new ServiceResponse<Order>();

      try
      {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
          throw new Exception("Not found.");

        order.Title = newTitle;

        await _context.SaveChangesAsync();

        serviceResponse.Data = order;
        serviceResponse.Message = $"Order '{id}' title has changed.";
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