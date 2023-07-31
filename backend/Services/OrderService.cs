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
        newOrderItem.EntryDate = DateTime.Now;
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

    public async Task<ServiceResponse<Order>> CompleteOrder(Order completeOrder)
    {
      var serviceResponse = new ServiceResponse<Order>();

      try
      {
        if (completeOrder.Completed == false)
          throw new Exception("Request to complete order is incorrect. 'Completed' key is false.");

        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == completeOrder.Id);
        var items = await _context.OrderItems.Where(item => item.OrderId == completeOrder.Id).ToListAsync();

        if (order == null)
          throw new Exception($"Order with Id '{completeOrder.Id}' was not found.");

        if (items != null)
        {
          foreach (OrderItem item in items)
          {
            item.EntryDate = DateTime.Now;

            var product = _mapper.Map<Product>(item);
            _context.Products.Add(product);
          }
        }
        else
        {
          serviceResponse.Message = $"Order with Id '{completeOrder.Id}' was completed. No items added to inventory.";
          _context.Orders.Remove(order);
          await _context.SaveChangesAsync();
          return serviceResponse;
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        serviceResponse.Message = $"Order with Id '{completeOrder.Id}' was completed. All items sent to inventory.";
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