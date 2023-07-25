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
      return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<Order>>> GetOrderById(int id)
    {
      var order = await _orderService.GetOrderById(id);

      if (order.Data == null)
      {
        return NotFound();
      }

      return Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<Order>>>> CreateNewOrder(Order newOrder)
    {
      var orders = await _orderService.CreateNewOrder(newOrder);

      return Created("order", orders);
    }
    
    [HttpPut]
    public async Task<ActionResult<ServiceResponse<List<Order>>>> UpdateOrder(Order updatedOrder)
    {
      var orders = await _orderService.UpdateOrder(updatedOrder);

      return Created("order", orders);
    }
  }
}