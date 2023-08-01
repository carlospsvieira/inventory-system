using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_system.Services
{
  public class OrderItemService : IOrderItemService
  {
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public OrderItemService(IMapper mapper, DataContext context)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<ServiceResponse<OrderItem>> AddOrderItem(int orderId, OrderItem newOrderItem)
    {
      var serviceResponse = new ServiceResponse<OrderItem>();

      try
      {
        newOrderItem.OrderId = orderId;
        newOrderItem.EntryDate = DateTime.Now;
        _context.OrderItems.Add(newOrderItem);

        await _context.SaveChangesAsync();

        serviceResponse.Data = newOrderItem;
        serviceResponse.Message = $"Item has been added to Order '{orderId}'";
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