using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace inventory_system.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class OrderItemController : ControllerBase
  {
    private readonly IOrderItemService _orderItemService;
    public OrderItemController(IOrderItemService orderItemService)
    {
      _orderItemService = orderItemService;

    }

    [HttpPost("{orderId}")]
    public async Task<ActionResult<ServiceResponse<OrderItem>>> AddOrderItem(int orderId, OrderItem newOrderItem)
    {
      var item = await _orderItemService.AddOrderItem(orderId, newOrderItem);

      if (item.Success == false)
      {
        return NotFound(item);
      }

      return Created("order", item);
    }

    [HttpPut("edit")]
    public async Task<ActionResult<ServiceResponse<OrderItem>>> UpdateOrderItem(OrderItem updatedItem)
    {
      var item = await _orderItemService.UpdateOrderItem(updatedItem);

      if (item.Success == false)
      {
        return NotFound(item);
      }

      return Ok(item);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<OrderItem>>> DeleteOrderItem(int id)
    {
      var item = await _orderItemService.DeleteOrderItem(id);

      if (item.Success == false)
      {
        return NotFound(item);
      }

      return Ok(item);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<OrderItem>>> GetById(int id)
    {
      var item = await _orderItemService.GetOrderItemById(id);

      if (item.Success == false)
      {
        return NotFound(item);
      }

      return Ok(item);
    }
  }
}