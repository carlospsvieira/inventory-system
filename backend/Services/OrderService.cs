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
        if (newOrder.Items == null)
          throw new Exception("New Items was not found");

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
          throw new Exception($"Order with Id '{id}' was not found.");

        _context.Orders.Remove(order);

        await _context.SaveChangesAsync();

        var orders = await _context.Orders.ToListAsync();

        serviceResponse.Data = orders;
        serviceResponse.Message = $"Order with Id '{id}' was deleted.";
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
        serviceResponse.Message = "OK. Reminder: Order Items will always be null at this endpoint for better performance reasons";
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
          throw new Exception($"Order with Id '{id}' was not found.");

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

    public async Task<ServiceResponse<OrderItem>> AddOrderItem(int id, OrderItem newOrderItem)
    {
      var serviceResponse = new ServiceResponse<OrderItem>();

      try
      {
        newOrderItem.OrderId = id;
        _context.OrderItems.Add(newOrderItem);

        await _context.SaveChangesAsync();

        serviceResponse.Data = newOrderItem;
        serviceResponse.Message = $"Item added to Order with Id '{id}'";
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }
      return serviceResponse;
    }

    public async Task<ServiceResponse<OrderItem>> RemoveItemFromOrder(int itemId)
    {
      var serviceResponse = new ServiceResponse<OrderItem>();

      try
      {
        var item = await _context.OrderItems.FirstOrDefaultAsync(item => item.Id == itemId);
        
        if (item == null)
          throw new Exception($"Item with Id '{itemId}' was not found in Order.");

        _context.OrderItems.Remove(item);

        await _context.SaveChangesAsync();

        serviceResponse.Data = item;
        serviceResponse.Message = $"Item with Id '{itemId}' was deleted from Order.";
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