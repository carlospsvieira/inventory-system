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
    public async Task<ActionResult<List<Order>>> Get()
    {
      var orders = await _orderService.GetAllOrders();
      return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrderById(int id)
    {
      var order = await _orderService.GetOrderById(id);

      if (order == null)
      {
        return NotFound();
      }

      return Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<List<Order>>> CreateNewOrder(Order newOrder)
    {
      await _orderService.CreateNewOrder(newOrder);

      var orders = await _orderService.GetAllOrders();

      return Created("order", orders);
    }
  }
}