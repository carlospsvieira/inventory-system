using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace inventory_system.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class OrderController : ControllerBase
  {
    private readonly IOrderService _orderService;
    public OrderController(IOrderService orderService)
    {
      _orderService = orderService;

    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<Order>>>> Get()
    {
      var orders = await _orderService.GetAllOrders();

      if (orders.Success == false)
      {
        return BadRequest(orders);
      }
      return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<Order>>> GetOrderById(int id)
    {
      var order = await _orderService.GetOrderById(id);

      if (order.Success == false)
      {
        return NotFound(order);
      }

      return Ok(order);
    }

    [HttpPost("new")]
    public async Task<ActionResult<ServiceResponse<Order>>> CreateNewOrder(Order newOrder)
    {
      var order = await _orderService.CreateNewOrder(newOrder);

      if (order.Success == false)
      {
        return BadRequest(order);
      }

      return Created("order", order);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<List<Order>>>> DeleteOrder(int id)
    {
      var orders = await _orderService.DeleteOrder(id);

      if (orders.Success == false)
      {
        return NotFound(orders);
      }

      return Ok(orders);
    }

    [HttpPost("complete")]
    public async Task<ActionResult<ServiceResponse<Order>>> CompleteOrder(Order completeOrder)
    {
      var order = await _orderService.CompleteOrder(completeOrder);

      if (order.Success == false)
      {
        return NotFound(order);
      }

      return Ok(order);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<ServiceResponse<Order>>> ChangeOrderTitle(int id, string newTitle)
    {
      var order = await _orderService.ChangeOrderTitle(id, newTitle);

      if (order.Success == false)
      {
        return BadRequest(order);
      }

      return Ok(order);
    }
  }
}