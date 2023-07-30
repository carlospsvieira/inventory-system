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

    public async Task<ServiceResponse<List<Order>>> CreateNewOrder(Order newOrder)
    {
      var serviceResponse = new ServiceResponse<List<Order>>();

      try
      {
        if (newOrder.Product == null)
          throw new Exception("New Product was not found");

        newOrder.Product.EntryDate = DateTime.Now;

        await _context.Orders.AddAsync(newOrder);

        await _context.SaveChangesAsync();

        var orders = await _context.Orders.ToListAsync();

        serviceResponse.Data = orders;
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
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);

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

    public async Task<ServiceResponse<Order>> UpdateOrder(Order updatedOrder)
    {
      var serviceResponse = new ServiceResponse<Order>();

      try
      {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == updatedOrder.Id);

        if (order == null)
          throw new Exception($"Order with Id '{updatedOrder.Id}' was not found.");

        _mapper.Map(updatedOrder, order);

        if (order.Product == null)
          throw new Exception("Updated Product was not found");

        order.Product.EntryDate = DateTime.Now;

        await _context.SaveChangesAsync();

        serviceResponse.Data = order;
        serviceResponse.Message = $"Order with Id '{updatedOrder.Id}' was updated.";
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